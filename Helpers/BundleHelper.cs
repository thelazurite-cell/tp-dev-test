using Microsoft.Extensions.DependencyInjection;
using NUglify.Css;
using NUglify.JavaScript;
using WebOptimizer;

namespace DevTest.Helpers
{
    public static class BundleHelper
    {
        public static void RegisterBundles(IAssetPipeline pipeline)
        {
            var javascriptSettings = new CodeSettings
            {
                StrictMode = false
            };
            var cssSettings = new CssSettings();

            #region Core Plugings

            pipeline.AddJavaScriptBundle("/js/bundles/core-plugins.min.js",
                "js/plugins/axios-0.18.0.min.js",
                "js/vee-validate.min.js",
                "js/vue-plugin-setup-core.js").FingerprintUrls().MinifyJavaScript(javascriptSettings);

            pipeline.AddCssBundle("/css/bundles/public-plugins.min.css",
                cssSettings,
                "js/plugins/elementui-2-15-7/theme-chalk/index.css"
                ).MinifyCss();

            #endregion
        }
    }
}