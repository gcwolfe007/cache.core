﻿using System;
using System.Runtime.Remoting.Messaging; 
using Unity.Lifetime;

namespace Sixeyed.Core.Containers
{
    /// <summary>
    /// Lifetime manager which uses an object for the life of the current call context
    /// </summary>
    /// <remarks>
    /// Typically used for objects which are to be created and used within the scope
    /// of a web request
    /// </remarks>
    public class PerCallContextLifeTimeManager : LifetimeManager
    {
        private readonly string _key = string.Format("PerCallContextLifeTimeManager_{0}", Guid.NewGuid());
        private LifetimeManager _lifetimeManagerImplementation;

        /// <summary>
        /// Gets the object from <see cref="CallContext"/>
        /// </summary>
        /// <returns></returns>
        public  object GetValue()
        {
            return CallContext.GetData(_key);
        }

        /// <summary>
        /// Sets the object in <see cref="CallContext"/>
        /// </summary>
        /// <param name="newValue"></param>
        public void SetValue(object newValue)
        {
            CallContext.SetData(_key, newValue);
        }

        /// <summary>
        /// Removes the object from <see cref="CallContext"/>
        /// </summary>
        public void RemoveValue()
        {
            CallContext.FreeNamedDataSlot(_key);
        }

        protected override LifetimeManager OnCreateLifetimeManager()
        {
            return (PerCallContextLifeTimeManager) _lifetimeManagerImplementation;
        }
    }
}
