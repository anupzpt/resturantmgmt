using DMS.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace DMS.UI.Services.Helper
{
    public static class GetAllControllerAndAction
    {
        public static List<ControllerActionVM> getAllControllerActionList()
        {
            Assembly asm = Assembly.GetAssembly(typeof(MvcApplication));
            var controlleractionlist = asm.GetTypes()
                .Where(type => typeof(System.Web.Mvc.Controller).IsAssignableFrom(type) )
                .SelectMany(type => type.GetMethods(BindingFlags.Instance  | BindingFlags.Public)) //| BindingFlags.DeclaredOnly
                .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                .Where(m=> !m.IsDefined(typeof(NonActionAttribute)))
                .Select(x => new
                {
                    Controller = x.DeclaringType.Name,
                    Action = x.Name,
                    ReturnType = x.ReturnType.Name,
                    Attributes = String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", "")))
                })
                .OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList();
            List<ControllerActionVM> _List = new List<ControllerActionVM>();
            foreach (var item in controlleractionlist)
            {
                ControllerActionVM _ControllerActionName = new ControllerActionVM()
                {
                    ControllerName = item.Controller,
                    ActionName = item.Action,
                    Attributes = item.Attributes,
                    ReturnType = item.ReturnType
                };
                _List.Add(_ControllerActionName);
            }

            return _List;
        }
    }
}