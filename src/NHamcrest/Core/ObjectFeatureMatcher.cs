using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NHamcrest.Core
{
    internal class ObjectFeatureMatcher<T> : IObjectFeatureMatcher<T>
    {
        private readonly string _describePrefix;
        private readonly string _mismatchPrefix;
        private readonly Type _valueType;
        private const int Indent = 4;
        private readonly List<IMatcher<T>> _matchers = new List<IMatcher<T>>();

        public ObjectFeatureMatcher(IEnumerable<IMatcher<T>> innerMatchers)
            : this(typeof(T), innerMatchers)
        {
        }

        public ObjectFeatureMatcher(Type valueType, IEnumerable<IMatcher<T>> innerMatchers)
            : this("a(n)", "was a(n)", valueType, innerMatchers)
        {
        }

        public ObjectFeatureMatcher(string describePrefix, string mismatchPrefix, Type valueType, IEnumerable<IMatcher<T>> innerMatchers)
        {
            _describePrefix = describePrefix;
            _mismatchPrefix = mismatchPrefix;
            _valueType = valueType;
            _matchers.AddRange(innerMatchers);
        }

        public void DescribeTo(IDescription description)
        {
            description.AppendText(_describePrefix)
                .AppendText(" ")
                .AppendText(typeof(T).Name);

            if (typeof(T) != _valueType)
            {
                description
                    .AppendText(" {")
                    .AppendText(_valueType.Name)
                    .AppendText("}");
            }

            if (_matchers.Count > 0)
                description.AppendText(" where:");

            DescribeMatchers(description, _matchers, (d, m) => d.AppendDescriptionOf(m));
        }

        private void DescribeMatchers(IDescription description, IEnumerable<IMatcher<T>> matchers, Action<IDescription, IMatcher<T>> matcherRenderer)
        {
            using (description.IndentBy(Indent))
            {
                foreach (var matcher in matchers)
                {
                    description.AppendNewLine();
                    matcherRenderer(description, matcher);
                }
            }
        }

        private bool Matches(T o, IDescription mismatchDescription)
        {
            if (o == null)
            {
                mismatchDescription.AppendText("was null");
                return false;
            }

            var failedMatchers = _matchers.Where(m => !m.Matches(o)).ToArray();
            if (failedMatchers.Length == 0)
                return true;

            mismatchDescription.AppendText(_mismatchPrefix)
                .AppendText(" ")
                .AppendText(typeof(T).Name);

            if (typeof(T) != o.GetType())
            {
                mismatchDescription
                    .AppendText(" {")
                    .AppendText(o.GetType().Name)
                    .AppendText("}");
            }
            mismatchDescription.AppendText(" where:");

            DescribeMatchers(mismatchDescription, failedMatchers, (d, m) => m.DescribeMismatch(o, d));

            return false;
        }

        public bool Matches(T item)
        {
            return Matches(item, new NullDescription());
        }

        public void DescribeMismatch(T item, IDescription mismatchDescription)
        {
            Matches(item, mismatchDescription);
        }

        private IObjectFeatureMatcher<T> WithFeatureMatcher(IMatcher<T> matcher)
        {
            var featureMatchers = _matchers.Concat(new[] {matcher});
            return new ObjectFeatureMatcher<T>(_describePrefix, _mismatchPrefix, typeof(T), featureMatchers);
        }

        public IObjectFeatureMatcher<T> Cast<TCast>(Func<IObjectFeatureMatcher<TCast>, IObjectFeatureMatcher<TCast>> castBuilder)
        {
            var emptyMatcher = new ObjectFeatureMatcher<TCast>("subtype", "subtype", typeof(TCast), Enumerable.Empty<IMatcher<TCast>>());
            var castMatcher = new CastMatcher<T, TCast>(castBuilder(emptyMatcher));

            return WithFeatureMatcher(castMatcher);
        }

        public IObjectFeatureMatcher<T> Property<Y, Z>(Expression<Func<T, Y>> propertyPath, IMatcher<Z> propertyMatcher)
            where Y : Z
        {
            return WithFeatureMatcher(new PropertyMatcher<T, Y, Z>(propertyPath, propertyMatcher));
        }

        public override string ToString()
        {
            var description = new StringDescription();
            DescribeTo(description);
            return description.ToString();
        }
    }
}