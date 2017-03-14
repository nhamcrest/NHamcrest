using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NHamcrest.Core
{
    internal class PropertyMatcher<TObject, TProperty, TMatcher> : IMatcher<TObject>
        where TProperty : TMatcher
    {
        private readonly IMatcher<TMatcher> _propertyMatcher;
        private readonly string _property;
        private readonly Func<TObject, TProperty> _propertyAccessor;

        public PropertyMatcher(Expression<Func<TObject, TProperty>> propertyPath, IMatcher<TMatcher> propertyMatcher)
        {
            _propertyMatcher = propertyMatcher;

            _property = GetPath(propertyPath);
            _propertyAccessor = propertyPath.Compile();
        }

        private bool Matches(TObject o, IDescription mismatchDescription)
        {
            try
            {
                var value = _propertyAccessor(o);

                if (!_propertyMatcher.Matches(value))
                {
                    mismatchDescription.AppendText("member {0} value ", _property);
                    _propertyMatcher.DescribeMismatch(value, mismatchDescription);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                mismatchDescription.AppendText("and exception has been thrown while accessing property {0}:", _property)
                    .AppendNewLine()
                    .AppendText(ex.ToString());

                return false;
            }
        }

        public void DescribeTo(IDescription description)
        {
            description.AppendText("member {0} value is ", _property)
                .AppendDescriptionOf(_propertyMatcher);
        }

        public bool Matches(TObject item)
        {
            return Matches(item, new NullDescription());
        }

        public void DescribeMismatch(TObject item, IDescription mismatchDescription)
        {
            Matches(item, mismatchDescription);
        }

        private static string GetPath(Expression<System.Func<TObject, TProperty>> expr)
        {
            var stack = new Stack<string>();

            MemberExpression me;
            switch (expr.Body.NodeType)
            {
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                    var ue = expr.Body as UnaryExpression;
                    me = ((ue != null) ? ue.Operand : null) as MemberExpression;
                    break;
                default:
                    me = expr.Body as MemberExpression;
                    break;
            }

            while (me != null)
            {
                stack.Push(me.Member.Name);
                me = me.Expression as MemberExpression;
            }

            return string.Join(".", stack.ToArray());
        }
    }
}
