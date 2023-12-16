using System.Collections;

namespace AsteroidsTest.Game
{
    public interface IPerishable
    {
        IEnumerator DeactivateAfterTime(float timeToDeactivate);
    }
}
