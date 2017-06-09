using System;
using System.Linq.Expressions;
using System.Reflection;

namespace ForumDal.Repositories
{
    class ParameterTypeModifier : ExpressionVisitor
    {
        private readonly Type srcParameterType;
        private readonly ParameterExpression destParameter; 

        public ParameterTypeModifier(Type srcParameterType, ParameterExpression parameter)
        {
            this.srcParameterType = srcParameterType;
            this.destParameter = parameter;
        }

        public Expression Modify(Expression expression)
        {
            return Visit(expression);
        }

        protected override Expression VisitParameter(ParameterExpression parameter)
        {
            if (parameter.Type == srcParameterType)
                return destParameter;

            return base.VisitParameter(parameter);
        }

        protected override Expression VisitMember(MemberExpression member)
        {
            if (member.Expression.Type == srcParameterType)
            {
                MemberInfo[] members = destParameter.Type.GetMember(member.Member.Name);
                if (members.Length != 1)
                    throw new InvalidCastException($"Cann't casts {srcParameterType} to {destParameter.Type} in expression tree. {destParameter.Type} has many {member.Member.Name} members or hasn't it.");
                return Expression.MakeMemberAccess(this.Visit(member.Expression), members[0]);
            }
            return base.VisitMember(member);
        }
    }
}
