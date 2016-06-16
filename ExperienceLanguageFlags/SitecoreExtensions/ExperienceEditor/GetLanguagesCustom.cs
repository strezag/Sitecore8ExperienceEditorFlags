using Sitecore.Data.Items;
using Sitecore.ExperienceEditor.Speak.Ribbon.Requests.ChangeLanguage;
using Sitecore.ExperienceEditor.Speak.Server.Responses;
using Sitecore.ExperienceEditor.Utils;
using System;
using System.Linq;

namespace ExperienceLanguageFlags.SitecoreExtensions.ExperienceEditor
{
    public class GetLanguagesCustom : GetLanguages
    {
        /// <summary>
        /// Override for Experience Editor Language Switching to include the Regional Iso Code
        /// Original: Sitecore.ExperienceEditor.Speak.Ribbon.Requests.ChangeLanguage.GetLanguages
        /// </summary>
        /// <param name="listDataSourceItem"></param>
        /// <returns>PipelineProcessorResponseValue</returns>
        protected override PipelineProcessorResponseValue GetChildItems(Sitecore.Data.Items.Item listDataSourceItem)
        {
            PipelineProcessorResponseValue pipelineProcessorResponseValue = new PipelineProcessorResponseValue()
            {
                Value = listDataSourceItem.GetChildren()
                .Where<Item>(new Func<Item, bool>(ItemUtility.CanSwitchLanguage))
                .Select((Item child) =>
                    new
                    {
                        Title = this.GetTitle(child),
                        ItemId = child.ID.ToString(),
                        IconPath = this.GetIcon(child),
                        /// Added Language code value for JSON response
                        LanguageCode = this.GetLanguageCode(child)
                    }).ToList()
            };
            return pipelineProcessorResponseValue;
        }

        /// <summary>
        ///  Gets Language Code and returns formatted string value. 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string GetLanguageCode(Item item)
        {
            /// Attempt getting value of 'Regional Iso Code' 
            string isoCode = item.Fields["Regional Iso Code"].Value.ToString();

            /// Determine if empty from 'Regional Iso Code' and attempt getting field 'Iso' if so
            isoCode = !String.IsNullOrEmpty(isoCode) ? isoCode : item.Fields["Iso"].Value.ToString();

            /// Format and return
            return !String.IsNullOrEmpty(isoCode) ? String.Format("({0})", isoCode.ToLower()) : String.Empty;
        }
    }
}