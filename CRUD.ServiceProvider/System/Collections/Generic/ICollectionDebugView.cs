using Newtonsoft.Json.Linq;

namespace CRUD.ServiceProvider.System.Collections.Generic
{
    internal class ICollectionDebugView<T>
    {
        private IList<JToken> childrenTokens;

        public ICollectionDebugView(IList<JToken> childrenTokens)
        {
            this.childrenTokens = childrenTokens;
        }
    }
}