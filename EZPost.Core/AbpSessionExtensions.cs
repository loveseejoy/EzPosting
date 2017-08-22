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
        /// 获取登录用户的LocationCode
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static string GetLocationCode(this IAbpSession session)
        {
            return GetClaimValue(ClaimConst.LocationCode);
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
