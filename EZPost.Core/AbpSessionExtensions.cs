using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Abp.Runtime.Session;
using System.Security.Claims;
using EZPost.Const;

namespace EZPost
{
     public static class AbpSessionExtensions
    {
        /// <summary>
        /// 获取代理id
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
         public static string GetAgentId(this IAbpSession session)
        {
            return GetClaimValue(ClaimConst.AgentId);
        }

        /// <summary>
        /// 获取客户id
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static string GetCustomerId(this IAbpSession session)
        {
            return GetClaimValue(ClaimConst.CustomerId);
        }

        /// <summary>
        /// 获取登录用户的LocationId
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static string GetLocationId(this IAbpSession session)
        {
            return GetClaimValue(ClaimConst.LocationId);
        }

        /// <summary>
        /// 获取登录用户的Gmt
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static string GetLocationGmt(this IAbpSession session)
        {
            return GetClaimValue(ClaimConst.Gmt);
        }

        /// <summary>
        /// 获取登录用户的LocationCode
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static string GetLocationCode(this IAbpSession session)
        {
            return GetClaimValue(ClaimConst.LocationCode);
        }


        /// <summary>
        /// 获取登录用户的LocationName
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static string GetLocationName(this IAbpSession session)
        {
            return GetClaimValue(ClaimConst.LocationName);
        }

        private static string GetClaimValue(string claimType)
         {
             var claimPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
             if (claimPrincipal == null)
             {
                 return null;
             }

             var claim = claimPrincipal.Claims.FirstOrDefault(x => x.Type == claimType);
             if (claim == null || string.IsNullOrEmpty(claim.Value))
             {
                 return null;
             }
             return claim.Value;
         }
    }
}
