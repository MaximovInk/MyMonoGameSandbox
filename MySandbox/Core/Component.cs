using Microsoft.Xna.Framework;
using System;

namespace MySandbox.Core
{
    [Serializable]
    public abstract class Component
    {
        public GameObject @object { get; protected set; }

        public bool is_enabled = true;

        public void Construct(GameObject @object)
        {
            this.@object = @object;
            if(is_enabled)
                OnConstruct();
        }

        public abstract void OnConstruct();

        public void Update(GameTime gameTime)
        {
            if (is_enabled)
                OnUpdate(gameTime);
        }

        protected abstract void OnUpdate(GameTime gameTime);

        
    }
}
