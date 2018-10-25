using AutoDeployClient.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoDeployClient.Utils
{
    public class ADCExtension
    {
        public static bool IsStatusNotNull(List<ADC_Status> adcStatusList, int adcIndex)
        {
            return adcStatusList.Where(adcStatusItem => adcStatusItem.ADC_Index == adcIndex).FirstOrDefault() != null ? true : false;
        }

        public static ADC_Status GetStatusItem(List<ADC_Status> adcStatusList, int adcIndex)
        {
            return adcStatusList.Where(adcStatusItem => adcStatusItem.ADC_Index == adcIndex).FirstOrDefault();
        }
    }
}