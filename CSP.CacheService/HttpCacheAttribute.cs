using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using CSP.Common.Contracts;

namespace CSP.CacheService
{
    /// <summary>
    /// Caches the supplied item and value in the Http cache
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class HttpCacheAttribute : FilterAttribute, IActionFilter
    {
        private int _duration;
        private ICacheService _cacheService;
        private string _name = null;

        /// <summary>
        /// Initializes a new instance of the HttpCache Attribute with the duration.
        /// </summary>
        /// <param name="duration">The duration. The default value is 10 minutes</param>
        public HttpCacheAttribute(int duration = 60 * 10)
        {
            this.Duration = duration;
            this.CacheService = new HttpCacheService();
        }

        /// <summary>
        /// Initializes a new instance of the HttpCache Attribute with the duration and cache service.
        /// </summary>
        /// <param name="duration">The duration.</param>
        /// <param name="cacheService">The cache service you want to use.</param>
        public HttpCacheAttribute(int duration, ICacheService cacheService)
        {
            this.Duration = duration;

            if (cacheService == null)
            {
                throw  new ArgumentNullException("Cache Service is null");
            }

            this.CacheService = cacheService;
        }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public int Duration
        {
            get
            {
                return this._duration;
            }

            set
            {
                this._duration = value;
            }
        }

        /// <summary>
        /// Gets or sets the cache service.
        /// </summary>
        /// <value>
        /// The cache service.
        /// </value>
        public ICacheService CacheService
        {
            get
            {
                return this._cacheService;
            }

            set
            {
                if (value != null)
                {
                    this._cacheService = value;
                }
            }
        }

        /// <summary>
        /// Called before an action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ICollection<string> actionResultParameters = filterContext
                .ActionParameters
                .Where(p => p.Value != null)
                .Select(p => p.Value.ToString())
                .ToList();

            this._name = string.Concat(filterContext.Controller, filterContext.ActionDescriptor.ActionName, string.Join("_", actionResultParameters));

            filterContext.Result = this.CacheService.Get(this._name, () => filterContext.Result, this.Duration);
        }

        /// <summary>
        /// Called after the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            this.CacheService.Get(this._name, () => filterContext.Result, this.Duration);
        }
    }
}
