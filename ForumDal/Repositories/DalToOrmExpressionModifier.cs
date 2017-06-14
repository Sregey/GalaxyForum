using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using ForumDal.Interface.Models;
using ForumOrm.Models;

namespace ForumDal.Repositories
{
    class DalToOrmExpressionModifier : ExpressionVisitor
    {
        private static readonly Dictionary<Type, Type> mapper = new Dictionary<Type, Type>()
        {
            { typeof(DalUser), typeof(User) },
            { typeof(DalTopic), typeof(Topic) },
            { typeof(DalComment), typeof(Comment) },
            { typeof(DalRole), typeof(Role) },
            { typeof(DalSection), typeof(Section) },
            { typeof(DalStatus), typeof(Status) },
            { typeof(DalImage), typeof(Image) },
        };

        //private readonly Type srcParameterType;
        private readonly ParameterExpression destParameter; 

        public DalToOrmExpressionModifier(ParameterExpression parameter)
        {
            //this.srcParameterType = srcParameterType;
            this.destParameter = parameter;
        }

        public Expression Modify(Expression expression)
        {
            return Visit(expression);
        }

        protected override Expression VisitParameter(ParameterExpression parameter)
        {
            if (mapper.ContainsKey(parameter.Type))
                return destParameter;

            return base.VisitParameter(parameter);
        }

        protected override Expression VisitMember(MemberExpression member)
        {
            Type exprType = member.Expression.Type;
            if (mapper.ContainsKey(exprType))
            {
                MemberInfo[] members = mapper[exprType].GetMember(member.Member.Name);
                if (members.Length != 1)
                    throw new InvalidCastException($"Cann't casts {exprType} to {mapper[exprType]} in expression tree. {mapper[exprType]} has many {member.Member.Name} members or hasn't it.");
                return Expression.MakeMemberAccess(this.Visit(member.Expression), members[0]);
            }
            return base.VisitMember(member);
        }
    }
}
