using Sitecore.ExperienceEditor.Speak.Ribbon.Requests.ChangeLanguage;
using Sitecore.ExperienceEditor.Speak.Server.Responses;
using Sitecore.ExperienceEditor.Utils;
using Sitecore.Resources;
using Sitecore.Web.UI;

namespace ExperienceLanguageFlags.SitecoreExtensions.ExperienceEditor
{
    /// <summary>
    /// LanguageIconLabelRequest Override
    /// 
    /// </summary>
    public class LanguageIconLabelRequestCustom : LanguageIconLabelRequest
    {
        public override PipelineProcessorResponseValue ProcessRequest()
        {
            PipelineProcessorResponseValue pipelineProcessorResponseValue = new PipelineProcessorResponseValue()
            {

                Value =
                new
                {
                    icon = Images.GetThemedImageSource(base.RequestContext.Item.Language.GetIcon(base.RequestContext.Item.Database), ImageDimension.id24x24),
                    label = StringUtility.RemoveRegionInfoInBraces(base.RequestContext.Item.Language.CultureInfo.DisplayName)
                }
            };

            return pipelineProcessorResponseValue;
        }
    }
}