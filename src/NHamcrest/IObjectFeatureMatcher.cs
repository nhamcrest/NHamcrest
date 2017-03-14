using System.Linq.Expressions;

namespace NHamcrest
{
    public interface IObjectFeatureMatcher<T> : IMatcher<T>
    {
        IObjectFeatureMatcher<T> Cast<TCast>(System.Func<IObjectFeatureMatcher<TCast>, IObjectFeatureMatcher<TCast>> castBuilder);

        IObjectFeatureMatcher<T> Property<Y, Z>(Expression<System.Func<T, Y>> propertyPath, IMatcher<Z> propertyMatcher)
            where Y : Z;
    }
}