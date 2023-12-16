using System.Collections;

namespace AsteroidsTest.Core
{
    public interface IPerishable
    {
        IEnumerator DeactivateAfterTime(float timeToDeactivate);
    }
}
