using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;
using WebApiClient.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace WebApiClient.Tools.Swagger.Models
{
    [HttpHost("http://iot.taichuan.net:80")]
    public interface IApplicationApi : IHttpApi
    {
        /// <summary>
        /// 设备测试
        /// 将自定义数据发送到应用下的设备，设备收之后无条件将原始数据返回
        /// </summary>
        /// <param name="num">设备机身号</param>
        /// <param name="data">数据内容</param>
        /// <returns>OK</returns>
        [HttpPost("v1/Device/SysTest")]
        ITask<ApiResultOfString> SysTestAsync([Required] string num, [Required] [JsonContent] StringData data);
        /// <summary>
        /// 设备系统控制
        /// 控制应用下的设备的开关和启用禁用
        /// </summary>
        /// <param name="num">设备机身号</param>
        /// <param name="data">控制内容</param>
        /// <returns>OK</returns>
        [HttpPost("v1/Device/SysCtrl")]
        ITask<ApiResultOfBoolean> SysCtrlAsync([Required] string num, [Required] [JsonContent] SysCtrlSetting data);
        /// <summary>
        /// 绑定设备到应用下
        /// 设备绑定到应用之后，才可以使用机身号与硬件码登录连接到硬件平台
        /// </summary>
        /// <param name="num">设备机身号</param>
        /// <param name="code">设备硬件码</param>
        /// <returns>OK</returns>
        [HttpPost("v1/Application/BindDevice")]
        ITask<ApiResultOfBoolean> BindDeviceAsync([Required] string num, [Required] string code);
        /// <summary>
        /// 通过机身号获取设备
        /// 返回应用下单个设备的基本信息
        /// </summary>
        /// <param name="num">设备机身号</param>
        /// <returns>OK</returns>
        [HttpGet("v1/Application/GetDevice")]
        ITask<ApiResultOfDeviceInfo> GetDeviceAsync([Required] string num);
        /// <summary>
        /// 通过机身号解绑设备
        /// 解绑之后的设备，可以重新绑定到其它应用之下
        /// </summary>
        /// <param name="num">设备机身号</param>
        /// <returns>OK</returns>
        [HttpDelete("v1/Application/UnbindDevice")]
        ITask<ApiResultOfBoolean> UnbindDeviceAsync([Required] string num);
        /// <summary>
        /// 分页获取设备
        /// 返回应用下的多个设备的基本信息
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="orderBy">排序字符串</param>
        /// <returns>OK</returns>
        [HttpGet("v1/Application/GetDevicePage")]
        ITask<ApiResultOfPageOfDeviceInfo> GetDevicePageAsync([Required] int pageIndex, int? pageSize, string orderBy);
    }
    [HttpHost("http://iot.taichuan.net:80")]
    public interface ICtrlMachineApi : IHttpApi
    {
        /// <summary>
        /// 获取中控机下的所有设备列表
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="data_timeout">超时的秒数	如果在Timeout秒之后才收到数据则不处理</param>
        /// <returns>OK</returns>
        [HttpGet("v1/CtrlMachine/GetDeviceList")]
        ITask<ApiResultOfCMDeivceData> GetDeviceListAsync([Required] string num, int? data_timeout);
        /// <summary>
        /// 获取中控机的设定的所有情景模式
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="data_timeout">超时的秒数	如果在Timeout秒之后才收到数据则不处理</param>
        /// <returns>OK</returns>
        [HttpGet("v1/CtrlMachine/GetSceneList")]
        ITask<ApiResultOfCMSceneInfoOf> GetSceneListAsync([Required] string num, int? data_timeout);
        /// <summary>
        /// 启动中控机的情景模式
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">数据</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/StartScene")]
        ITask<ApiResultOfBoolean> StartSceneAsync([Required] string num, [Required] [JsonContent] CMSceneSetting model);
        /// <summary>
        /// 控制自定义智能设备 2
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMCustomTwoBody 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetCustomTwo")]
        ITask<ApiResultOfBoolean> SetCustomTwoAsync([Required] string num, [Required] [JsonContent] CMCustomTwoSetting model);
        /// <summary>
        /// 控制灯控开关
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMCustomTwoBody 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetLightControl")]
        ITask<ApiResultOfBoolean> SetLightControlAsync([Required] string num, [Required] [JsonContent] CMLightControlSetting model);
        /// <summary>
        /// 控制调光开关
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMDimmerBody 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetDimmer")]
        ITask<ApiResultOfBoolean> SetDimmerAsync([Required] string num, [Required] [JsonContent] CMDimmerSetting model);
        /// <summary>
        /// 控制空调开关
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMAirSwitchBody 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetAirSwitch")]
        ITask<ApiResultOfBoolean> SetAirSwitchAsync([Required] string num, [Required] [JsonContent] CMAirSwitchSetting model);
        /// <summary>
        /// 控制插座
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMOutletBody 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetOutlet")]
        ITask<ApiResultOfBoolean> SetOutletAsync([Required] string num, [Required] [JsonContent] CMOutletSetting model);
        /// <summary>
        /// 控制窗帘
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMCurtainBody 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetCurtain")]
        ITask<ApiResultOfBoolean> SetCurtainAsync([Required] string num, [Required] [JsonContent] CMCurtainSetting model);
        /// <summary>
        /// 控制紧急按钮
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMSecurityBody 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetUrgentButton")]
        ITask<ApiResultOfBoolean> SetUrgentButtonAsync([Required] string num, [Required] [JsonContent] CMSecuritySetting model);
        /// <summary>
        /// 控制红外报警
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMSecurityBody 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetInfraredAlarm")]
        ITask<ApiResultOfBoolean> SetInfraredAlarmAsync([Required] string num, [Required] [JsonContent] CMSecuritySetting model);
        /// <summary>
        /// 控制烟雾报警
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMSecurityBody 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetSmokeAlarm")]
        ITask<ApiResultOfBoolean> SetSmokeAlarmAsync([Required] string num, [Required] [JsonContent] CMSecuritySetting model);
        /// <summary>
        /// 控制瓦斯报警
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMSecurityBody 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetGasAlarm")]
        ITask<ApiResultOfBoolean> SetGasAlarmAsync([Required] string num, [Required] [JsonContent] CMSecuritySetting model);
        /// <summary>
        /// 控制门磁报警
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMSecurityBody 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetDoorAlarm")]
        ITask<ApiResultOfBoolean> SetDoorAlarmAsync([Required] string num, [Required] [JsonContent] CMSecuritySetting model);
        /// <summary>
        /// 控制窗磁报警
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMSecurityBody 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetWindowAlarm")]
        ITask<ApiResultOfBoolean> SetWindowAlarmAsync([Required] string num, [Required] [JsonContent] CMSecuritySetting model);
        /// <summary>
        /// 控制通用报警
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMSecurityBody 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetGeneralAlarm")]
        ITask<ApiResultOfBoolean> SetGeneralAlarmAsync([Required] string num, [Required] [JsonContent] CMSecuritySetting model);
        /// <summary>
        /// 控制无线红外设备
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMSecurityBody 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetWirelessInfrared")]
        ITask<ApiResultOfBoolean> SetWirelessInfraredAsync([Required] string num, [Required] [JsonContent] CMSecuritySetting model);
        /// <summary>
        /// 控制单火线无线灯控开关
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMTVControllerBody 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetSingleLight")]
        ITask<ApiResultOfBoolean> SetSingleLightAsync([Required] string num, [Required] [JsonContent] CMSingleLightSetting model);
        /// <summary>
        /// 恒亦明LED控制器
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMLedBody 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetLed")]
        ITask<ApiResultOfBoolean> SetLedAsync([Required] string num, [Required] [JsonContent] CMLedSetting model);
        /// <summary>
        /// 控制中央空调
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMCentralAir 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetCentralAir")]
        ITask<ApiResultOfBoolean> SetCentralAirAsync([Required] string num, [Required] [JsonContent] CMCentralAirSetting model);
        /// <summary>
        /// 控制新风系统设备
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMFreshAirSetting 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetFreshAir")]
        ITask<ApiResultOfBoolean> SetFreshAirAsync([Required] string num, [Required] [JsonContent] CMFreshAirSetting model);
        /// <summary>
        /// 控制百朗新风系统设备
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMBLFreshAirSetting 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetBLFreshAir")]
        ITask<ApiResultOfBoolean> SetBLFreshAirAsync([Required] string num, [Required] [JsonContent] CMBLFreshAirSetting model);
        /// <summary>
        /// 控制触摸灯控设备
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMTouchLightSetting 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetTouchLight")]
        ITask<ApiResultOfBoolean> SetTouchLightAsync([Required] string num, [Required] [JsonContent] CMTouchLightSetting model);
        /// <summary>
        /// 智能门锁一键开门
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">控制数据</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SmartLockOpen")]
        ITask<ApiResultOfBoolean> SmartLockOpenAsync([Required] string num, [Required] [JsonContent] CMCtrlRequest model);
        /// <summary>
        /// 智能门锁-钥匙管理
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMSmartLockSetting 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetSmartLock")]
        ITask<ApiResultOfBoolean> SetSmartLockAsync([Required] string num, [Required] [JsonContent] CMSmartLockSetting model);
        /// <summary>
        /// 智能门锁-获取钥匙列表
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">请求数据</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/GetSmartLockList")]
        ITask<ApiResultOfCMSmartLockBodyOf> GetSmartLockListAsync([Required] string num, [Required] [JsonContent] CMCtrlRequest model);
        /// <summary>
        /// 智能门锁-临时密码管理
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMTempPassWordSetting2 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetTempPassWord")]
        ITask<ApiResultOfBoolean> SetTempPassWordAsync([Required] string num, [Required] [JsonContent] CMTempPassWordSetting2 model);
        /// <summary>
        /// 智能门锁-获取临时密码列表
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMCtrlRequest 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/GetTempPassWordList")]
        ITask<ApiResultOfCMTempPassWordBodyOf> GetTempPassWordListAsync([Required] string num, [Required] [JsonContent] CMCtrlRequest model);
        /// <summary>
        /// 控制风扇设备
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMFanBody 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetFan")]
        ITask<ApiResultOfBoolean> SetFanAsync([Required] string num, [Required] [JsonContent] CMFanSetting model);
        /// <summary>
        /// 控制声光报警器
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMSecurityBody 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetAcoustoAlarm")]
        ITask<ApiResultOfBoolean> SetAcoustoAlarmAsync([Required] string num, [Required] [JsonContent] CMSecuritySetting model);
        /// <summary>
        /// 控制无线门铃
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetDoorbell")]
        ITask<ApiResultOfBoolean> SetDoorbellAsync([Required] string num, [Required] [JsonContent] CMDoorbellSetting model);
        /// <summary>
        /// 控制免扰开关
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMImmunityBody 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetImmunity")]
        ITask<ApiResultOfBoolean> SetImmunityAsync([Required] string num, [Required] [JsonContent] CMImmunitySetting model);
        /// <summary>
        /// 控制灯控总开关
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMLampMasterBody 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetLampMaster")]
        ITask<ApiResultOfBoolean> SetLampMasterAsync([Required] string num, [Required] [JsonContent] CMLampMasterSetting model);
        /// <summary>
        /// 设置中兴德舜地暖
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="data">CMZTEGroundHeatingSetting 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/GetHub10A")]
        ITask<ApiResultOfBoolean> GetHub10AAsync([Required] string num, [Required] [JsonContent] CMZTEGroundHeatingSetting data);
        /// <summary>
        /// 获取红外空调按键学习状态
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="data_timeout">超时的秒数	如果在Timeout秒之后才收到数据则不处理</param>
        /// <param name="data_id">中控机的设备id</param>
        /// <returns>OK</returns>
        [HttpGet("v1/CtrlMachine/GetInfAirKeys")]
        ITask<ApiResultOfCMInfAirKeyStateOf> GetInfAirKeysAsync([Required] string num, int? data_timeout, [Required] string data_id);
        /// <summary>
        /// 学习红外空调按键
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="data">空调控制数据实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/LearnInfAirKey")]
        ITask<ApiResultOfBoolean> LearnInfAirKeyAsync([Required] string num, [Required] [JsonContent] CMInfAirKeyCtrl data);
        /// <summary>
        /// 控制红外空调按键
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="data">空调控制数据实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetInfAir")]
        ITask<ApiResultOfBoolean> SetInfAirAsync([Required] string num, [Required] [JsonContent] CMInfAirKeyCtrl data);
        /// <summary>
        /// 获取红外电视按键学习状态
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="data_timeout">超时的秒数	如果在Timeout秒之后才收到数据则不处理</param>
        /// <param name="data_id">中控机的设备id</param>
        /// <returns>OK</returns>
        [HttpGet("v1/CtrlMachine/GetInfTvKeys")]
        ITask<ApiResultOfCMInfTvKeyStateOf> GetInfTvKeysAsync([Required] string num, int? data_timeout, [Required] string data_id);
        /// <summary>
        /// 学习电视按键
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="data">电视控制数据实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/LearnInfTvKey")]
        ITask<ApiResultOfBoolean> LearnInfTvKeyAsync([Required] string num, [Required] [JsonContent] CMInfTvKeyCtrl data);
        /// <summary>
        /// 控制红外电视按键
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="data">电视控制数据实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetInfTv")]
        ITask<ApiResultOfBoolean> SetInfTvAsync([Required] string num, [Required] [JsonContent] CMInfTvKeyCtrl data);
        /// <summary>
        /// 获取红外机顶盒按键学习状态
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="data_timeout">超时的秒数	如果在Timeout秒之后才收到数据则不处理</param>
        /// <param name="data_id">中控机的设备id</param>
        /// <returns>OK</returns>
        [HttpGet("v1/CtrlMachine/GetInfTopboxKeys")]
        ITask<ApiResultOfCMInfTopboxKeyStateOf> GetInfTopboxKeysAsync([Required] string num, int? data_timeout, [Required] string data_id);
        /// <summary>
        /// 学习机顶盒按键
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="data">机顶盒控制数据实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/LearnInfTopboxKey")]
        ITask<ApiResultOfBoolean> LearnInfTopboxKeyAsync([Required] string num, [Required] [JsonContent] CMInfTopboxKeyCtrl data);
        /// <summary>
        /// 控制红外机顶盒按键
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="data">机顶盒控制数据实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetInfTopbox")]
        ITask<ApiResultOfBoolean> SetInfTopboxAsync([Required] string num, [Required] [JsonContent] CMInfTopboxKeyCtrl data);
        /// <summary>
        /// 获取红外背景音乐按键学习状态
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="data_timeout">超时的秒数	如果在Timeout秒之后才收到数据则不处理</param>
        /// <param name="data_id">中控机的设备id</param>
        /// <returns>OK</returns>
        [HttpGet("v1/CtrlMachine/GetInfDvdKeys")]
        ITask<ApiResultOfCMInfDvdKeyStateOf> GetInfDvdKeysAsync([Required] string num, int? data_timeout, [Required] string data_id);
        /// <summary>
        /// 学习背景音乐按键
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="data">背景音乐控制数据实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/LearnInfDvdKey")]
        ITask<ApiResultOfBoolean> LearnInfDvdKeyAsync([Required] string num, [Required] [JsonContent] CMInfDvdKeyCtrl data);
        /// <summary>
        /// 控制红外背景音乐按键
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="data">背景音乐控制数据实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetInfDvd")]
        ITask<ApiResultOfBoolean> SetInfDvdAsync([Required] string num, [Required] [JsonContent] CMInfDvdKeyCtrl data);
        /// <summary>
        /// 获取红外 [自定义遥控器1] 按键学习状态
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="data_timeout">超时的秒数	如果在Timeout秒之后才收到数据则不处理</param>
        /// <param name="data_id">中控机的设备id</param>
        /// <returns>OK</returns>
        [HttpGet("v1/CtrlMachine/GetInfTeleOneKeys")]
        ITask<ApiResultOfCMInfTeleOneKeyStateOf> GetInfTeleOneKeysAsync([Required] string num, int? data_timeout, [Required] string data_id);
        /// <summary>
        /// 学习 [自定义遥控器1] 按键
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="data">[自定义遥控器1] 控制数据实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/LearnInfTeleOneKey")]
        ITask<ApiResultOfBoolean> LearnInfTeleOneKeyAsync([Required] string num, [Required] [JsonContent] CMInfTeleOneKeyCtrl data);
        /// <summary>
        /// 控制红外 [自定义遥控器1] 按键
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="data">[自定义遥控器1] 控制数据实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetInfTeleOne")]
        ITask<ApiResultOfBoolean> SetInfTeleOneAsync([Required] string num, [Required] [JsonContent] CMInfTeleOneKeyCtrl data);
        /// <summary>
        /// 获取红外 [自定义遥控器2] 按键学习状态
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="data_timeout">超时的秒数	如果在Timeout秒之后才收到数据则不处理</param>
        /// <param name="data_id">中控机的设备id</param>
        /// <returns>OK</returns>
        [HttpGet("v1/CtrlMachine/GetInfTeleTwoKeys")]
        ITask<ApiResultOfCMInfTeleTwoKeyStateOf> GetInfTeleTwoKeysAsync([Required] string num, int? data_timeout, [Required] string data_id);
        /// <summary>
        /// 学习 [自定义遥控器2] 按键
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="data">[自定义遥控器2] 控制数据实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/LearnInfTeleTwoKey")]
        ITask<ApiResultOfBoolean> LearnInfTeleTwoKeyAsync([Required] string num, [Required] [JsonContent] CMInfTeleTwoKeyCtrl data);
        /// <summary>
        /// 控制红外 [自定义遥控器2] 按键
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="data">[自定义遥控器2] 控制数据实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetInfTeleTwo")]
        ITask<ApiResultOfBoolean> SetInfTeleTwoAsync([Required] string num, [Required] [JsonContent] CMInfTeleTwoKeyCtrl data);
        /// <summary>
        /// 控制大金中央空调
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="data">CMDaiKinAirSetting 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetDaiKinAir")]
        ITask<ApiResultOfBoolean> SetDaiKinAirAsync([Required] string num, [Required] [JsonContent] CMDaiKinAirSetting data);
        /// <summary>
        /// 设置中兴德舜风盘
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="data">CMZTEFanCoilSetting 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetZTEFanCoil")]
        ITask<ApiResultOfBoolean> SetZTEFanCoilAsync([Required] string num, [Required] [JsonContent] CMZTEFanCoilSetting data);
        /// <summary>
        /// 设置中兴德舜空调
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="data">CMZTEAirSetting 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetZTEAir")]
        ITask<ApiResultOfBoolean> SetZTEAirAsync([Required] string num, [Required] [JsonContent] CMZTEAirSetting data);
        /// <summary>
        /// 设置中兴德舜地暖
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="data">CMZTEGroundHeatingSetting 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetZTEGroundHeating")]
        ITask<ApiResultOfBoolean> SetZTEGroundHeatingAsync([Required] string num, [Required] [JsonContent] CMZTEGroundHeatingSetting data);
        /// <summary>
        /// 10A/16A计量插座 - 查询当前功率
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">控制数据</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/GetHubPower")]
        ITask<ApiResultOfInt16> GetHubPowerAsync([Required] string num, [Required] [JsonContent] CMHubPower model);
        /// <summary>
        /// 10A计量插座 - 设置开关
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMCustomTwoBody 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetHub10ASwitch")]
        ITask<ApiResultOfBoolean> SetHub10ASwitchAsync([Required] string num, [Required] [JsonContent] CMOutletSetting model);
        /// <summary>
        /// 16A计量插座 - 设置开关
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMCustomTwoBody 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetHub16ASwitch")]
        ITask<ApiResultOfBoolean> SetHub16ASwitchAsync([Required] string num, [Required] [JsonContent] CMOutletSetting model);
        /// <summary>
        /// 美的中央空调控制
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetMideaCentralAir")]
        ITask<ApiResultOfBoolean> SetMideaCentralAirAsync([Required] string num, [Required] [JsonContent] CMMideaCentralAirSetting data);
        /// <summary>
        /// 控制背景音乐设备
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMBackgroundMusicSetting 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetBackgroundMusic")]
        ITask<ApiResultOfBoolean> SetBackgroundMusicAsync([Required] string num, [Required] [JsonContent] CMBackgroundMusicSetting model);
        /// <summary>
        /// 日立中央空调控制
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMHitachiCentralAirSetting 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetHitachiCentralAir")]
        ITask<ApiResultOfBoolean> SetHitachiCentralAirAsync([Required] string num, [Required] [JsonContent] CMHitachiCentralAirSetting model);
        /// <summary>
        /// 意爱浦新风控制
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMYiupFreshAirSetting 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetYiupFreshAir")]
        ITask<ApiResultOfBoolean> SetYiupFreshAirAsync([Required] string num, [Required] [JsonContent] CMYiupFreshAirSetting model);
        /// <summary>
        /// 合生-地暖控制
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMHesGroundHeatingControl 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetHesGroundHeatingControl")]
        ITask<ApiResultOfBoolean> SetHesGroundHeatingControlAsync([Required] string num, [Required] [JsonContent] CMHesGroundHeatingControl model);
        /// <summary>
        /// 合生-地暖设置
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMHesGroundHeatingSetting 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetHesGroundHeatingSetting")]
        ITask<ApiResultOfBoolean> SetHesGroundHeatingSettingAsync([Required] string num, [Required] [JsonContent] CMHesGroundHeatingSetting model);
        /// <summary>
        /// 合生门锁一键开锁
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="model">CMHesSmartLockControl 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/SetHesSmartLockControl")]
        ITask<ApiResultOfBoolean> SetHesSmartLockControlAsync([Required] string num, [Required] [JsonContent] CMHesSmartLockControl model);
    }
    [HttpHost("http://iot.taichuan.net:80")]
    public interface ICMApi : IHttpApi
    {
        /// <summary>
        /// 设置空调开关(3路)
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="data">CMAirSwitch3Setting 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/CM_SetAirSwitch3")]
        ITask<ApiResultOfBoolean> SetAirSwitch3Async([Required] string num, [Required] [JsonContent] CMAirSwitch3Setting data);
        /// <summary>
        /// 设置地暧开关(1路)
        /// </summary>
        /// <param name="num">中控机机身号</param>
        /// <param name="data">CMGroundHeatingSwitchSetting 实体</param>
        /// <returns>OK</returns>
        [HttpPost("v1/CtrlMachine/CM_SetGroundHeating")]
        ITask<ApiResultOfBoolean> SetGroundHeatingAsync([Required] string num, [Required] [JsonContent] CMGroundHeatingSwitchSetting data);
    }
    [HttpHost("http://iot.taichuan.net:80")]
    public interface IEntranceMachineApi : IHttpApi
    {
        /// <summary>
        /// 发送自定义数据到应用下的门口机
        /// 并等待门口机的回执
        /// 此接口用于扩展开发门口机的功能
        /// </summary>
        /// <param name="num">设备机身号</param>
        /// <param name="data">数据内容</param>
        /// <returns>OK</returns>
        [HttpPost("v1/EntranceMachine/Send")]
        ITask<ApiResultOfBoolean> SendAsync([Required] string num, [Required] [JsonContent] TimeoutDataSetting data);
        /// <summary>
        /// 投递自定义数据到应用下的门口机
        /// 但不等待门口机的回执
        /// 此接口用于扩展开发门口机的功能
        /// </summary>
        /// <param name="num">设备机身号</param>
        /// <param name="data">数据内容</param>
        /// <returns>OK</returns>
        [HttpPost("v1/EntranceMachine/Post")]
        ITask<ApiResultOfBoolean> PostAsync([Required] string num, [Required] [JsonContent] StringData data);
        /// <summary>
        /// 投递自定义数据到应用下的多个门口机
        /// 但不等待门口机的回执
        /// 此接口用于扩展开发门口机的功能
        /// </summary>
        /// <param name="data">数据内容</param>
        /// <returns>OK</returns>
        [HttpPost("v1/EntranceMachine/PostBat")]
        ITask<ApiResultOfBoolean> PostBatAsync([Required] [JsonContent] StringDataBat data);
        /// <summary>
        /// 投递自定义数据到应用下有效的多个门口机
        /// 但不等待门口机的回执
        /// 返回无效的机身号集合
        /// </summary>
        /// <param name="data">数据内容</param>
        /// <returns>OK</returns>
        [HttpPost("v1/EntranceMachine/PostBatWithoutValidation")]
        ITask<ApiResultOfStringOf> PostBatWithoutValidationAsync([Required] [JsonContent] StringDataBat data);
        /// <summary>
        /// 门口机开锁
        /// 当门口机5s内收到此请求，将进行开锁动作
        /// </summary>
        /// <param name="num">门口机机身号</param>
        /// <returns>OK</returns>
        [HttpPost("v1/EntranceMachine/Unlock")]
        ITask<ApiResultOfBoolean> UnlockAsync([Required] string num);
        /// <summary>
        /// 门口机开锁
        /// 可附加自定义内容，超时时间可调整
        /// </summary>
        /// <param name="num">门口机机身号</param>
        /// <param name="data">控制参数</param>
        /// <returns>OK</returns>
        [HttpPost("v1/EntranceMachine/UnlockEx")]
        ITask<ApiResultOfBoolean> UnlockExAsync([Required] string num, [Required] [JsonContent] TimeoutDataSetting data);
    }
    [HttpHost("http://iot.taichuan.net:80")]
    public interface IIntercomApi : IHttpApi
    {
        /// <summary>
        /// 获取对讲账号的详细信息
        /// </summary>
        /// <param name="userId">对讲账号的Id</param>
        /// <returns>OK</returns>
        [HttpGet("v1/Intercom/GetAccount")]
        ITask<ApiResultOfIntercomAccount_Dto> GetAccountAsync([Required] string userId);
        /// <summary>
        /// 创建对讲账号
        /// 对讲账号在设备或手机初始化后，只要知道对方的对讲userId，就可以向对讲发起对讲请求
        /// 智能家居设备无对讲功能，不需要为它的设备申请账号
        /// </summary>
        /// <param name="userId">对讲账号的Id，数字、字母（区分大小写）组成，最长31位，开发者自定义，一个应用内需要唯一</param>
        /// <returns>OK</returns>
        [HttpPost("v1/Intercom/CreateAccount")]
        ITask<ApiResultOfIntercomAccount_Dto> CreateAccountAsync([Required] string userId);
    }
    [HttpHost("http://iot.taichuan.net:80")]
    public interface IRemotePushApi : IHttpApi
    {
        /// <summary>
        /// 创建远程终端的推送账号
        /// 终端通过账号与token进行登录
        /// </summary>
        /// <returns>OK</returns>
        [HttpPost("v1/RemotePush/CreateAccount")]
        ITask<ApiResultOfPushAccount_Dto> CreateAccountAsync();
        /// <summary>
        /// 发送自定义数据到应用下的远程终端
        /// 并等待远程终端的回执
        /// </summary>
        /// <param name="id">终端推送id</param>
        /// <param name="data">数据内容</param>
        /// <returns>OK</returns>
        [HttpPost("v1/RemotePush/Send")]
        ITask<ApiResultOfBoolean> SendAsync([Required] string id, [Required] [JsonContent] TimeoutDataSetting data);
        /// <summary>
        /// 投递自定义数据到应用下的远程终端
        /// 但不等待远程终端的回执
        /// </summary>
        /// <param name="id">终端推送id</param>
        /// <param name="data">数据内容</param>
        /// <returns>OK</returns>
        [HttpPost("v1/RemotePush/Post")]
        ITask<ApiResultOfBoolean> PostAsync([Required] string id, [Required] [JsonContent] StringData data);
        /// <summary>
        /// 投递自定义数据到应用下的远程终端
        /// 但不等待远程终端的回执
        /// </summary>
        /// <param name="data">数据内容</param>
        /// <returns>OK</returns>
        [HttpPost("v1/RemotePush/PostBat")]
        ITask<ApiResultOfBoolean> PostBatAsync([Required] [JsonContent] StringDataPushBat data);
    }
    /// <summary>表示文本内容</summary>
    public partial class StringData
    {
        /// <summary>数据内容</summary>
        [AliasAs("data")]
        [Required(AllowEmptyStrings = true)]
        public string Data { get; set; }
    }
    /// <summary>表示Api结果</summary>
    public partial class ApiResultOfString
    {
        /// <summary>错误码</summary>
        [AliasAs("code")]
        public ApiResultOfStringCode Code { get; set; }
        /// <summary>相关提示信息</summary>
        [AliasAs("msg")]
        [Required(AllowEmptyStrings = true)]
        public string Msg { get; set; }
        /// <summary>业务数据</summary>
        [AliasAs("data")]
        [Required(AllowEmptyStrings = true)]
        public string Data { get; set; }
    }
    /// <summary>系统控制</summary>
    public partial class SysCtrlSetting
    {
        /// <summary>超时的秒数
        /// 如果设备在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>控制码</summary>
        [AliasAs("ctrlCode")]
        public SysCtrlSettingCtrlCode? CtrlCode { get; set; }
    }
    /// <summary>表示Api结果</summary>
    public partial class ApiResultOfBoolean
    {
        /// <summary>错误码</summary>
        [AliasAs("code")]
        public ApiResultOfBooleanCode Code { get; set; }
        /// <summary>相关提示信息</summary>
        [AliasAs("msg")]
        [Required(AllowEmptyStrings = true)]
        public string Msg { get; set; }
        /// <summary>业务数据</summary>
        [AliasAs("data")]
        public bool Data { get; set; }
    }
    /// <summary>表示Api结果</summary>
    public partial class ApiResultOfDeviceInfo
    {
        /// <summary>错误码</summary>
        [AliasAs("code")]
        public ApiResultOfDeviceInfoCode Code { get; set; }
        /// <summary>相关提示信息</summary>
        [AliasAs("msg")]
        [Required(AllowEmptyStrings = true)]
        public string Msg { get; set; }
        /// <summary>业务数据</summary>
        [AliasAs("data")]
        [Required]
        public DeviceInfo Data { get; set; } = new DeviceInfo();
    }
    /// <summary>设备信息</summary>
    public partial class DeviceInfo
    {
        /// <summary>ID</summary>
        [AliasAs("id")]
        [StringLength(50)]
        public string Id { get; set; }
        /// <summary>关联的应用ID</summary>
        [AliasAs("apP_Id")]
        [Required(AllowEmptyStrings = true)]
        [StringLength(50)]
        public string ApP_Id { get; set; }
        /// <summary>机身号</summary>
        [AliasAs("num")]
        [Required(AllowEmptyStrings = true)]
        [StringLength(50)]
        public string Num { get; set; }
        /// <summary>硬件码</summary>
        [AliasAs("code")]
        [Required(AllowEmptyStrings = true)]
        [StringLength(50)]
        public string Code { get; set; }
        /// <summary>设备类别，比如TC-U9D-H1</summary>
        [AliasAs("type")]
        [StringLength(50)]
        public string Type { get; set; }
        /// <summary>设备类别简写</summary>
        [AliasAs("shortType")]
        [StringLength(2)]
        public string ShortType { get; set; }
        /// <summary>设备所属的产品类型</summary>
        [AliasAs("productType")]
        public DeviceInfoProductType? ProductType { get; set; }
        /// <summary>创建时间</summary>
        [AliasAs("createTime")]
        [Required(AllowEmptyStrings = true)]
        public System.DateTime CreateTime { get; set; }
        /// <summary>是否在线</summary>
        [AliasAs("isOnline")]
        public bool? IsOnline { get; set; }
        /// <summary>ip地址</summary>
        [AliasAs("ipAddress")]
        [StringLength(50)]
        public string IpAddress { get; set; }
        /// <summary>ip端口</summary>
        [AliasAs("port")]
        public int? Port { get; set; }
        /// <summary>最近在线/离线时间</summary>
        [AliasAs("lastOnOffTime")]
        public System.DateTime? LastOnOffTime { get; set; }
    }
    /// <summary>表示Api结果</summary>
    public partial class ApiResultOfPageOfDeviceInfo
    {
        /// <summary>错误码</summary>
        [AliasAs("code")]
        public ApiResultOfPageOfDeviceInfoCode Code { get; set; }
        /// <summary>相关提示信息</summary>
        [AliasAs("msg")]
        [Required(AllowEmptyStrings = true)]
        public string Msg { get; set; }
        /// <summary>业务数据</summary>
        [AliasAs("data")]
        [Required]
        public PageOfDeviceInfo Data { get; set; } = new PageOfDeviceInfo();
    }
    /// <summary>表示分页内容</summary>
    public partial class PageOfDeviceInfo
    {
        /// <summary>全部记录条数</summary>
        [AliasAs("total")]
        public int Total { get; set; }
        /// <summary>页面索引，0开始</summary>
        [AliasAs("pageIndex")]
        public int PageIndex { get; set; }
        /// <summary>页面记录大小</summary>
        [AliasAs("pageSize")]
        public int PageSize { get; set; }
        /// <summary>数据集合</summary>
        [AliasAs("dataArray")]
        [Required]
        public System.Collections.Generic.List<DeviceInfo> DataArray { get; set; } = new System.Collections.Generic.List<DeviceInfo>();
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfTimeoutRequest
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfTimeoutRequestApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfTimeoutRequestMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public TimeoutRequest Body { get; set; }
    }
    /// <summary>表示超时的Body</summary>
    public partial class TimeoutRequest
    {
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMDeivceData
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMDeivceDataApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMDeivceDataMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMDeivceData Body { get; set; }
    }
    /// <summary>中控机的设备数据</summary>
    public partial class CMDeivceData
    {
        /// <summary>获取或设置所有设备</summary>
        [AliasAs("devices")]
        public System.Collections.Generic.List<CMDeviceInfo> Devices { get; set; }
        /// <summary>获取或设置所有房间</summary>
        [AliasAs("rooms")]
        public System.Collections.Generic.List<CMRoomInfo> Rooms { get; set; }
    }
    /// <summary>中控机的设备</summary>
    public partial class CMDeviceInfo
    {
        /// <summary>标识符</summary>
        [AliasAs("id")]
        public string Id { get; set; }
        /// <summary>名称</summary>
        [AliasAs("name")]
        public string Name { get; set; }
        /// <summary>类型</summary>
        [AliasAs("type")]
        public CMDeviceInfoType? Type { get; set; }
        /// <summary>所在房间Id</summary>
        [AliasAs("roomId")]
        public int? RoomId { get; set; }
        /// <summary>开关状态</summary>
        [AliasAs("switchState")]
        public CMDeviceInfoSwitchState? SwitchState { get; set; }
        /// <summary>核心参数类型</summary>
        [AliasAs("coreParamType")]
        public CMDeviceInfoCoreParamType? CoreParamType { get; set; }
        /// <summary>核心参数值</summary>
        [AliasAs("coreParamvalue")]
        public int? CoreParamvalue { get; set; }
    }
    /// <summary>中控机的房间</summary>
    public partial class CMRoomInfo
    {
        /// <summary>房间的id</summary>
        [AliasAs("id")]
        public string Id { get; set; }
        /// <summary>房间的名称</summary>
        [AliasAs("name")]
        public string Name { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMSceneInfoOf
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMSceneInfoOfApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMSceneInfoOfMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public System.Collections.Generic.List<CMSceneInfo> Body { get; set; }
    }
    /// <summary>中控机情景模式</summary>
    public partial class CMSceneInfo
    {
        /// <summary>标识符</summary>
        [AliasAs("id")]
        public string Id { get; set; }
        /// <summary>名称</summary>
        [AliasAs("name")]
        public string Name { get; set; }
        /// <summary>情景模式类型</summary>
        [AliasAs("mode")]
        public CMSceneInfoMode? Mode { get; set; }
        /// <summary>是否为当前情景</summary>
        [AliasAs("isActive")]
        public bool? IsActive { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMSceneSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMSceneSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMSceneSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMSceneSetting Body { get; set; }
    }
    /// <summary>情景模式设置</summary>
    public partial class CMSceneSetting
    {
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>应用的情景模式id</summary>
        [AliasAs("sceneId")]
        [Required(AllowEmptyStrings = true)]
        public string SceneId { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfBoolean
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfBooleanApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfBooleanMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public bool? Body { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMAlarmInfo
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMAlarmInfoApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMAlarmInfoMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMAlarmInfo Body { get; set; }
    }
    /// <summary>中控机设备报警</summary>
    public partial class CMAlarmInfo
    {
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        public string Id { get; set; }
        /// <summary>中控机的设备类型</summary>
        [AliasAs("type")]
        public CMAlarmInfoType? Type { get; set; }
        /// <summary>报警时间
        /// yyyy-MM-dd HH:mm:ss</summary>
        [AliasAs("createTime")]
        public string CreateTime { get; set; }
        /// <summary>报警内容</summary>
        [AliasAs("alarmContent")]
        public string AlarmContent { get; set; }
        /// <summary>报警类型</summary>
        [AliasAs("alertType")]
        public CMAlarmInfoAlertType? AlertType { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMOpenRecordInfo
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMOpenRecordInfoApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMOpenRecordInfoMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMOpenRecordInfo Body { get; set; }
    }
    /// <summary>智能家居-开锁记录</summary>
    public partial class CMOpenRecordInfo
    {
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        public string Id { get; set; }
        /// <summary>中控机的设备类型</summary>
        [AliasAs("type")]
        public CMOpenRecordInfoType? Type { get; set; }
        /// <summary>钥匙标签</summary>
        [AliasAs("label")]
        public string Label { get; set; }
        /// <summary>开门类型</summary>
        [AliasAs("openType")]
        public CMOpenRecordInfoOpenType? OpenType { get; set; }
        /// <summary>开门时间</summary>
        [AliasAs("openTime")]
        public System.DateTime? OpenTime { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMLedSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMLedSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMLedSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMLedSetting Body { get; set; }
    }
    /// <summary>恒亦明LED控制器</summary>
    public partial class CMLedSetting
    {
        /// <summary>控制参数</summary>
        [AliasAs("ledEnums")]
        public CMLedSettingLedEnums? LedEnums { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMCustomTwoSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMCustomTwoSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMCustomTwoSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMCustomTwoSetting Body { get; set; }
    }
    /// <summary>自定义智能设备 2</summary>
    public partial class CMCustomTwoSetting
    {
        /// <summary>控制参数</summary>
        [AliasAs("customTwo")]
        public CMCustomTwoSettingCustomTwo? CustomTwo { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMLightControlSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMLightControlSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMLightControlSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMLightControlSetting Body { get; set; }
    }
    /// <summary>灯控开关</summary>
    public partial class CMLightControlSetting
    {
        /// <summary>开关指令</summary>
        [AliasAs("lightSwitch")]
        public CMLightControlSettingLightSwitch? LightSwitch { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMDimmerSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMDimmerSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMDimmerSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMDimmerSetting Body { get; set; }
    }
    /// <summary>可调灯光设备</summary>
    public partial class CMDimmerSetting
    {
        /// <summary>控制亮度值</summary>
        [AliasAs("brightness")]
        public CMDimmerSettingBrightness? Brightness { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMAirSwitchSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMAirSwitchSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMAirSwitchSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMAirSwitchSetting Body { get; set; }
    }
    /// <summary>空调开关</summary>
    public partial class CMAirSwitchSetting
    {
        /// <summary>控制指令</summary>
        [AliasAs("airSwitch")]
        public CMAirSwitchSettingAirSwitch? AirSwitch { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMOutletSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMOutletSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMOutletSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMOutletSetting Body { get; set; }
    }
    /// <summary>插座</summary>
    public partial class CMOutletSetting
    {
        /// <summary>开关指令</summary>
        [AliasAs("outletSwitch")]
        public CMOutletSettingOutletSwitch? OutletSwitch { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMCurtainSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMCurtainSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMCurtainSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMCurtainSetting Body { get; set; }
    }
    /// <summary>窗帘</summary>
    public partial class CMCurtainSetting
    {
        /// <summary>打开程度</summary>
        [AliasAs("schedule")]
        public CMCurtainSettingSchedule? Schedule { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMSecuritySetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMSecuritySettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMSecuritySettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMSecuritySetting Body { get; set; }
    }
    /// <summary>安防设备通用实体</summary>
    public partial class CMSecuritySetting
    {
        /// <summary>控制指令</summary>
        [AliasAs("state")]
        public CMSecuritySettingState? State { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMDoorbellSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMDoorbellSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMDoorbellSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMDoorbellSetting Body { get; set; }
    }
    /// <summary>无线门铃</summary>
    public partial class CMDoorbellSetting
    {
        /// <summary>开关控制 [开 true/关 false]</summary>
        [AliasAs("isOpen")]
        public bool? IsOpen { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMSingleLightSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMSingleLightSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMSingleLightSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMSingleLightSetting Body { get; set; }
    }
    /// <summary>单火线无线灯控开关</summary>
    public partial class CMSingleLightSetting
    {
        /// <summary>控制指令</summary>
        [AliasAs("isOpen")]
        public CMSingleLightSettingIsOpen? IsOpen { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMCentralAirSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMCentralAirSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMCentralAirSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMCentralAirSetting Body { get; set; }
    }
    /// <summary>中央空调</summary>
    public partial class CMCentralAirSetting
    {
        /// <summary>开关控制</summary>
        [AliasAs("centralAir")]
        public CMCentralAirSettingCentralAir? CentralAir { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMFreshAirSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMFreshAirSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMFreshAirSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMFreshAirSetting Body { get; set; }
    }
    /// <summary>新风系统设备</summary>
    public partial class CMFreshAirSetting
    {
        /// <summary>控制参数</summary>
        [AliasAs("freshAir")]
        public CMFreshAirSettingFreshAir? FreshAir { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMTouchLightSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMTouchLightSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMTouchLightSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMTouchLightSetting Body { get; set; }
    }
    /// <summary>触摸灯控</summary>
    public partial class CMTouchLightSetting
    {
        /// <summary>开关控制</summary>
        [AliasAs("lightSwitch")]
        public CMTouchLightSettingLightSwitch? LightSwitch { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMCtrlRequest
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMCtrlRequestApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMCtrlRequestMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMCtrlRequest Body { get; set; }
    }
    /// <summary>中控机下的设备控制请求</summary>
    public partial class CMCtrlRequest
    {
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMSmartLockSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMSmartLockSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMSmartLockSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMSmartLockSetting Body { get; set; }
    }
    /// <summary>智能门锁-钥匙管理</summary>
    public partial class CMSmartLockSetting
    {
        /// <summary>操作类型</summary>
        [AliasAs("actionType")]
        public CMSmartLockSettingActionType? ActionType { get; set; }
        /// <summary>钥匙Id</summary>
        [AliasAs("pwdId")]
        public string PwdId { get; set; }
        /// <summary>钥匙类型</summary>
        [AliasAs("keyType")]
        public CMSmartLockSettingKeyType KeyType { get; set; }
        /// <summary>标签</summary>
        [AliasAs("label")]
        public string Label { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMSmartLockBodyOf
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMSmartLockBodyOfApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMSmartLockBodyOfMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public System.Collections.Generic.List<CMSmartLockBody> Body { get; set; }
    }
    /// <summary>智能家居-智能门锁-钥匙实体</summary>
    public partial class CMSmartLockBody
    {
        /// <summary>设备ID</summary>
        [AliasAs("devId")]
        public string DevId { get; set; }
        /// <summary>钥匙Id</summary>
        [AliasAs("pwdId")]
        public string PwdId { get; set; }
        /// <summary>钥匙类型</summary>
        [AliasAs("keyType")]
        public CMSmartLockBodyKeyType KeyType { get; set; }
        /// <summary>标签</summary>
        [AliasAs("label")]
        public string Label { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMTempPassWordSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMTempPassWordSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMTempPassWordSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMTempPassWordSetting Body { get; set; }
    }
    /// <summary>智能门锁-临时密码管理</summary>
    public partial class CMTempPassWordSetting
    {
        /// <summary>操作类型</summary>
        [AliasAs("actionType")]
        public CMTempPassWordSettingActionType? ActionType { get; set; }
        /// <summary>密码ID</summary>
        [AliasAs("pwdID")]
        public string PwdID { get; set; }
        /// <summary>临时密码(不可重复,如重复不处理)</summary>
        [AliasAs("tempPWD")]
        public string TempPWD { get; set; }
        /// <summary>在指定的时间点生效
        /// (年月日 时分)</summary>
        [AliasAs("effectTime")]
        public System.DateTime? EffectTime { get; set; }
        /// <summary>有效时长(小时 0.5,1,2,3,4 小时)</summary>
        [AliasAs("effectiveTime")]
        public double? EffectiveTime { get; set; }
        /// <summary>有效次数(1-254)</summary>
        [AliasAs("effectiveNum")]
        public int? EffectiveNum { get; set; }
        /// <summary>临时密码状态</summary>
        [AliasAs("state")]
        public CMTempPassWordSettingState State { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMTempPassWordBodyOf
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMTempPassWordBodyOfApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMTempPassWordBodyOfMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public System.Collections.Generic.List<CMTempPassWordBody> Body { get; set; }
    }
    /// <summary>智能门锁-临时密码实体</summary>
    public partial class CMTempPassWordBody
    {
        /// <summary>设备ID</summary>
        [AliasAs("devId")]
        public string DevId { get; set; }
        /// <summary>密码ID</summary>
        [AliasAs("pwdID")]
        public string PwdID { get; set; }
        /// <summary>临时密码</summary>
        [AliasAs("tempPWD")]
        public string TempPWD { get; set; }
        /// <summary>生效时间</summary>
        [AliasAs("effectTime")]
        public System.DateTime? EffectTime { get; set; }
        /// <summary>有效时长（小时）</summary>
        [AliasAs("effectiveTime")]
        public int? EffectiveTime { get; set; }
        /// <summary>有效次数</summary>
        [AliasAs("effectiveNum")]
        public int? EffectiveNum { get; set; }
        /// <summary>临时密码状态</summary>
        [AliasAs("state")]
        public CMTempPassWordBodyState State { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMBLFreshAirSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMBLFreshAirSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMBLFreshAirSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMBLFreshAirSetting Body { get; set; }
    }
    /// <summary>百朗新风系统</summary>
    public partial class CMBLFreshAirSetting
    {
        /// <summary>开关控制</summary>
        [AliasAs("blFreshAir")]
        public CMBLFreshAirSettingBlFreshAir? BlFreshAir { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMFanSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMFanSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMFanSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMFanSetting Body { get; set; }
    }
    /// <summary>风扇</summary>
    public partial class CMFanSetting
    {
        /// <summary>风速档/关 [0-5,表示关,1-5 表示开]</summary>
        [AliasAs("windSpeed")]
        public int? WindSpeed { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMImmunitySetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMImmunitySettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMImmunitySettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMImmunitySetting Body { get; set; }
    }
    /// <summary>免扰开关</summary>
    public partial class CMImmunitySetting
    {
        /// <summary>免扰开关</summary>
        [AliasAs("immunityEnums")]
        public CMImmunitySettingImmunityEnums? ImmunityEnums { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMLampMasterSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMLampMasterSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMLampMasterSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMLampMasterSetting Body { get; set; }
    }
    /// <summary>灯控总开关</summary>
    public partial class CMLampMasterSetting
    {
        /// <summary>开关控制</summary>
        [AliasAs("lampSwitch")]
        public CMLampMasterSettingLampSwitch? LampSwitch { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMInfAirKeyStateOf
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMInfAirKeyStateOfApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMInfAirKeyStateOfMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public System.Collections.Generic.List<CMInfAirKeyState> Body { get; set; }
    }
    /// <summary>红外空调按键学习状态</summary>
    public partial class CMInfAirKeyState
    {
        /// <summary>键</summary>
        [AliasAs("key")]
        public CMInfAirKeyStateKey? Key { get; set; }
        /// <summary>是否已学习</summary>
        [AliasAs("isLearned")]
        public bool? IsLearned { get; set; }
        /// <summary>键的显示名称</summary>
        [AliasAs("name")]
        public string Name { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMInfAirKeyCtrl
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMInfAirKeyCtrlApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMInfAirKeyCtrlMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMInfAirKeyCtrl Body { get; set; }
    }
    /// <summary>红外空调控制或学习</summary>
    public partial class CMInfAirKeyCtrl
    {
        /// <summary>控制按键</summary>
        [AliasAs("key")]
        public CMInfAirKeyCtrlKey? Key { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMInfTvKeyStateOf
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMInfTvKeyStateOfApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMInfTvKeyStateOfMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public System.Collections.Generic.List<CMInfTvKeyState> Body { get; set; }
    }
    /// <summary>红外电视按键学习状态</summary>
    public partial class CMInfTvKeyState
    {
        /// <summary>键</summary>
        [AliasAs("key")]
        public CMInfTvKeyStateKey? Key { get; set; }
        /// <summary>是否已学习</summary>
        [AliasAs("isLearned")]
        public bool? IsLearned { get; set; }
        /// <summary>键的显示名称</summary>
        [AliasAs("name")]
        public string Name { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMInfTvKeyCtrl
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMInfTvKeyCtrlApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMInfTvKeyCtrlMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMInfTvKeyCtrl Body { get; set; }
    }
    /// <summary>红外电视控制或学习</summary>
    public partial class CMInfTvKeyCtrl
    {
        /// <summary>控制按键</summary>
        [AliasAs("key")]
        public CMInfTvKeyCtrlKey? Key { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMInfTopboxKeyStateOf
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMInfTopboxKeyStateOfApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMInfTopboxKeyStateOfMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public System.Collections.Generic.List<CMInfTopboxKeyState> Body { get; set; }
    }
    /// <summary>红外机顶盒按键学习状态</summary>
    public partial class CMInfTopboxKeyState
    {
        /// <summary>键</summary>
        [AliasAs("key")]
        public CMInfTopboxKeyStateKey? Key { get; set; }
        /// <summary>是否已学习</summary>
        [AliasAs("isLearned")]
        public bool? IsLearned { get; set; }
        /// <summary>键的显示名称</summary>
        [AliasAs("name")]
        public string Name { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMInfTopboxKeyCtrl
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMInfTopboxKeyCtrlApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMInfTopboxKeyCtrlMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMInfTopboxKeyCtrl Body { get; set; }
    }
    /// <summary>红外电机顶盒控制或学习</summary>
    public partial class CMInfTopboxKeyCtrl
    {
        /// <summary>控制按键</summary>
        [AliasAs("key")]
        public CMInfTopboxKeyCtrlKey? Key { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMInfDvdKeyStateOf
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMInfDvdKeyStateOfApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMInfDvdKeyStateOfMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public System.Collections.Generic.List<CMInfDvdKeyState> Body { get; set; }
    }
    /// <summary>红外背景音乐按键学习状态</summary>
    public partial class CMInfDvdKeyState
    {
        /// <summary>键</summary>
        [AliasAs("key")]
        public CMInfDvdKeyStateKey? Key { get; set; }
        /// <summary>是否已学习</summary>
        [AliasAs("isLearned")]
        public bool? IsLearned { get; set; }
        /// <summary>键的显示名称</summary>
        [AliasAs("name")]
        public string Name { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMInfDvdKeyCtrl
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMInfDvdKeyCtrlApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMInfDvdKeyCtrlMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMInfDvdKeyCtrl Body { get; set; }
    }
    /// <summary>红外背景音乐控制或学习</summary>
    public partial class CMInfDvdKeyCtrl
    {
        /// <summary>控制按键</summary>
        [AliasAs("key")]
        public CMInfDvdKeyCtrlKey? Key { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMInfTeleOneKeyStateOf
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMInfTeleOneKeyStateOfApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMInfTeleOneKeyStateOfMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public System.Collections.Generic.List<CMInfTeleOneKeyState> Body { get; set; }
    }
    /// <summary>红外 [自定义遥控器1] 按键学习状态</summary>
    public partial class CMInfTeleOneKeyState
    {
        /// <summary>键</summary>
        [AliasAs("key")]
        public CMInfTeleOneKeyStateKey? Key { get; set; }
        /// <summary>是否已学习</summary>
        [AliasAs("isLearned")]
        public bool? IsLearned { get; set; }
        /// <summary>键的显示名称</summary>
        [AliasAs("name")]
        public string Name { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMInfTeleOneKeyCtrl
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMInfTeleOneKeyCtrlApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMInfTeleOneKeyCtrlMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMInfTeleOneKeyCtrl Body { get; set; }
    }
    /// <summary>红外 [自定义遥控器1] 控制或学习</summary>
    public partial class CMInfTeleOneKeyCtrl
    {
        /// <summary>控制按键</summary>
        [AliasAs("key")]
        public CMInfTeleOneKeyCtrlKey? Key { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMInfTeleTwoKeyStateOf
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMInfTeleTwoKeyStateOfApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMInfTeleTwoKeyStateOfMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public System.Collections.Generic.List<CMInfTeleTwoKeyState> Body { get; set; }
    }
    /// <summary>红外 [自定义遥控器2] 按键学习状态</summary>
    public partial class CMInfTeleTwoKeyState
    {
        /// <summary>键</summary>
        [AliasAs("key")]
        public CMInfTeleTwoKeyStateKey? Key { get; set; }
        /// <summary>是否已学习</summary>
        [AliasAs("isLearned")]
        public bool? IsLearned { get; set; }
        /// <summary>键的显示名称</summary>
        [AliasAs("name")]
        public string Name { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMInfTeleTwoKeyCtrl
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMInfTeleTwoKeyCtrlApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMInfTeleTwoKeyCtrlMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMInfTeleTwoKeyCtrl Body { get; set; }
    }
    /// <summary>红外 [自定义遥控器2] 控制或学习</summary>
    public partial class CMInfTeleTwoKeyCtrl
    {
        /// <summary>控制按键</summary>
        [AliasAs("key")]
        public CMInfTeleTwoKeyCtrlKey? Key { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMDaiKinAirSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMDaiKinAirSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMDaiKinAirSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMDaiKinAirSetting Body { get; set; }
    }
    /// <summary>大金中央空调</summary>
    public partial class CMDaiKinAirSetting
    {
        /// <summary>开关指令</summary>
        [AliasAs("daiKinAir")]
        public CMDaiKinAirSettingDaiKinAir? DaiKinAir { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMAirSwitch3Setting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMAirSwitch3SettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMAirSwitch3SettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMAirSwitch3Setting Body { get; set; }
    }
    /// <summary>空调开关(3路)</summary>
    public partial class CMAirSwitch3Setting
    {
        /// <summary>控制指令</summary>
        [AliasAs("airSwitch")]
        public CMAirSwitch3SettingAirSwitch? AirSwitch { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMGroundHeatingSwitchSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMGroundHeatingSwitchSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMGroundHeatingSwitchSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMGroundHeatingSwitchSetting Body { get; set; }
    }
    /// <summary>地暖开关</summary>
    public partial class CMGroundHeatingSwitchSetting
    {
        /// <summary>控制指令</summary>
        [AliasAs("switch")]
        public CMGroundHeatingSwitchSettingSwitch? Switch { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMZTEFanCoilSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMZTEFanCoilSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMZTEFanCoilSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMZTEFanCoilSetting Body { get; set; }
    }
    /// <summary>中兴德舜风盘</summary>
    public partial class CMZTEFanCoilSetting
    {
        /// <summary>控制指令</summary>
        [AliasAs("zteFanCoil")]
        public CMZTEFanCoilSettingZteFanCoil? ZteFanCoil { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMZTEAirSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMZTEAirSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMZTEAirSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMZTEAirSetting Body { get; set; }
    }
    /// <summary>中兴德舜风盘</summary>
    public partial class CMZTEAirSetting
    {
        /// <summary>控制指令</summary>
        [AliasAs("zteAir")]
        public CMZTEAirSettingZteAir? ZteAir { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMZTEGroundHeatingSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMZTEGroundHeatingSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMZTEGroundHeatingSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMZTEGroundHeatingSetting Body { get; set; }
    }
    /// <summary>中兴德舜地暖</summary>
    public partial class CMZTEGroundHeatingSetting
    {
        /// <summary>控制指令</summary>
        [AliasAs("zteGroundHeating")]
        public CMZTEGroundHeatingSettingZteGroundHeating? ZteGroundHeating { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMHubPower
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMHubPowerApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMHubPowerMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMHubPower Body { get; set; }
    }
    /// <summary>计量插座</summary>
    public partial class CMHubPower
    {
        /// <summary>计量插座类型:(10A_47、16A_48)
        /// <para></para><para></para></summary>
        [AliasAs("deviceType")]
        public CMHubPowerDeviceType? DeviceType { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfInt32
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfInt32Api? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfInt32Mode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public int? Body { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMHubUpPower
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMHubUpPowerApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMHubUpPowerMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMHubUpPower Body { get; set; }
    }
    /// <summary>设备每隔30分钟上报功率</summary>
    public partial class CMHubUpPower
    {
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        public string Id { get; set; }
        /// <summary>计量插座类型:(10A_47、16A_48)
        /// <para></para><para></para></summary>
        [AliasAs("type")]
        public CMHubUpPowerType? Type { get; set; }
        /// <summary>上报时间
        /// yyyy-MM-dd HH:mm:ss</summary>
        [AliasAs("createTime")]
        public string CreateTime { get; set; }
        /// <summary>功率 (单位：w)</summary>
        [AliasAs("value")]
        public int? Value { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMHubUpWh
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMHubUpWhApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMHubUpWhMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMHubUpWh Body { get; set; }
    }
    /// <summary>设备每隔30分钟上报用电量</summary>
    public partial class CMHubUpWh
    {
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        public string Id { get; set; }
        /// <summary>计量插座类型:(10A_47、16A_48)
        /// <para></para><para></para></summary>
        [AliasAs("type")]
        public CMHubUpWhType? Type { get; set; }
        /// <summary>报警时间
        /// yyyy-MM-dd HH:mm:ss</summary>
        [AliasAs("createTime")]
        public string CreateTime { get; set; }
        /// <summary>用电量 (单位: 千瓦时)</summary>
        [AliasAs("value")]
        public double? Value { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMMideaCentralAirSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMMideaCentralAirSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMMideaCentralAirSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMMideaCentralAirSetting Body { get; set; }
    }
    /// <summary>美的中央空调控制</summary>
    public partial class CMMideaCentralAirSetting
    {
        /// <summary>开或关
        /// 当值为关时，其它字段的值将无意义</summary>
        [AliasAs("cmSwitch")]
        public CMMideaCentralAirSettingCmSwitch CmSwitch { get; set; }
        /// <summary>温度值，可为null</summary>
        [AliasAs("temperature")]
        public int? Temperature { get; set; }
        /// <summary>模式，可为null</summary>
        [AliasAs("mode")]
        public CMMideaCentralAirSettingMode? Mode { get; set; }
        /// <summary>风速，可为null</summary>
        [AliasAs("speed")]
        public CMMideaCentralAirSettingSpeed? Speed { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMBackgroundMusicSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMBackgroundMusicSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMBackgroundMusicSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMBackgroundMusicSetting Body { get; set; }
    }
    /// <summary>背景音乐设备</summary>
    public partial class CMBackgroundMusicSetting
    {
        /// <summary>控制音乐
        /// 可为null</summary>
        [AliasAs("musicCtrl")]
        public CMBackgroundMusicSettingMusicCtrl? MusicCtrl { get; set; }
        /// <summary>音量调节
        /// 可为null</summary>
        [AliasAs("volume")]
        public int? Volume { get; set; }
        /// <summary>切换模式
        /// 可为null</summary>
        [AliasAs("switchMode")]
        public CMBackgroundMusicSettingSwitchMode? SwitchMode { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMHitachiCentralAirSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMHitachiCentralAirSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMHitachiCentralAirSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMHitachiCentralAirSetting Body { get; set; }
    }
    /// <summary>日立中央空调控制</summary>
    public partial class CMHitachiCentralAirSetting
    {
        /// <summary>控制空调
        /// 可空</summary>
        [AliasAs("cmSwitch")]
        public CMHitachiCentralAirSettingCmSwitch? CmSwitch { get; set; }
        /// <summary>温度
        /// 可空</summary>
        [AliasAs("temperature")]
        public int? Temperature { get; set; }
        /// <summary>模式调节
        /// 可空</summary>
        [AliasAs("mode")]
        public CMHitachiCentralAirSettingMode? Mode { get; set; }
        /// <summary>风速
        /// 可空</summary>
        [AliasAs("speed")]
        public CMHitachiCentralAirSettingSpeed? Speed { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMYiupFreshAirSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMYiupFreshAirSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMYiupFreshAirSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMYiupFreshAirSetting Body { get; set; }
    }
    /// <summary>意爱浦新风设置</summary>
    public partial class CMYiupFreshAirSetting
    {
        /// <summary>控制空调
        /// 可空</summary>
        [AliasAs("cmSwitch")]
        public CMYiupFreshAirSettingCmSwitch? CmSwitch { get; set; }
        /// <summary>风速调节
        /// 可空</summary>
        [AliasAs("windSpeed")]
        public CMYiupFreshAirSettingWindSpeed? WindSpeed { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMHesGroundHeatingControl
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMHesGroundHeatingControlApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMHesGroundHeatingControlMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMHesGroundHeatingControl Body { get; set; }
    }
    /// <summary>合生地暖-控制功能</summary>
    public partial class CMHesGroundHeatingControl
    {
        /// <summary>开启状态
        /// 可空</summary>
        [AliasAs("cmSwitch")]
        public CMHesGroundHeatingControlCmSwitch? CmSwitch { get; set; }
        /// <summary>开启温度
        /// 可空</summary>
        [AliasAs("temperature")]
        public int? Temperature { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMHesGroundHeatingSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMHesGroundHeatingSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMHesGroundHeatingSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMHesGroundHeatingSetting Body { get; set; }
    }
    /// <summary>合生地暖-设置功能</summary>
    public partial class CMHesGroundHeatingSetting
    {
        /// <summary>防冻状态
        /// 可空</summary>
        [AliasAs("cmSwitch")]
        public CMHesGroundHeatingSettingCmSwitch? CmSwitch { get; set; }
        /// <summary>防冻温度
        /// 可空</summary>
        [AliasAs("temperature")]
        public int? Temperature { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMHesSmartLockControl
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMHesSmartLockControlApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMHesSmartLockControlMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMHesSmartLockControl Body { get; set; }
    }
    /// <summary>合生-智能门锁</summary>
    public partial class CMHesSmartLockControl
    {
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfCMEnvironDetector
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfCMEnvironDetectorApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfCMEnvironDetectorMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public CMEnvironDetector Body { get; set; }
    }
    /// <summary>环境检测仪</summary>
    public partial class CMEnvironDetector
    {
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        public string Id { get; set; }
        /// <summary>设备类型
        /// <para></para><para></para></summary>
        [AliasAs("type")]
        public CMEnvironDetectorType? Type { get; set; }
        /// <summary>温度 单位 ℃</summary>
        [AliasAs("temperature")]
        public int? Temperature { get; set; }
        /// <summary>湿度 单位%</summary>
        [AliasAs("humidity")]
        public int? Humidity { get; set; }
        /// <summary>甲醛 mg/m3 浮点（两位小数）</summary>
        [AliasAs("formaldehyde")]
        public double? Formaldehyde { get; set; }
        /// <summary>PM2.5 单位μg/m3</summary>
        [AliasAs("pM25")]
        public int? PM25 { get; set; }
        /// <summary>TVOC mg/m3 浮点（两位小数）</summary>
        [AliasAs("tvoc")]
        public double? Tvoc { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfTimeoutDataSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfTimeoutDataSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfTimeoutDataSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public TimeoutDataSetting Body { get; set; }
    }
    /// <summary>超时数据请求</summary>
    public partial class TimeoutDataSetting
    {
        /// <summary>超时的秒数
        /// 如果设备在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>数据内容</summary>
        [AliasAs("data")]
        public string Data { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfString
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfStringApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfStringMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public string Body { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfUploadLogSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfUploadLogSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfUploadLogSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public UploadLogSetting Body { get; set; }
    }
    /// <summary>要求上传日志body</summary>
    public partial class UploadLogSetting
    {
        /// <summary>上传的日志级别
        /// 可组合的值</summary>
        [AliasAs("logLevel")]
        public UploadLogSettingLogLevel? LogLevel { get; set; }
        /// <summary>开始时间</summary>
        [AliasAs("beginTime")]
        public System.DateTime? BeginTime { get; set; }
        /// <summary>结束时间</summary>
        [AliasAs("endTime")]
        public System.DateTime? EndTime { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfSysCtrlSetting
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfSysCtrlSettingApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfSysCtrlSettingMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public SysCtrlSetting Body { get; set; }
    }
    /// <summary>表示Api结果</summary>
    public partial class ApiResultOfCMDeivceData
    {
        /// <summary>错误码</summary>
        [AliasAs("code")]
        public ApiResultOfCMDeivceDataCode Code { get; set; }
        /// <summary>相关提示信息</summary>
        [AliasAs("msg")]
        [Required(AllowEmptyStrings = true)]
        public string Msg { get; set; }
        /// <summary>业务数据</summary>
        [AliasAs("data")]
        [Required]
        public CMDeivceData Data { get; set; } = new CMDeivceData();
    }
    /// <summary>表示Api结果</summary>
    public partial class ApiResultOfCMSceneInfoOf
    {
        /// <summary>错误码</summary>
        [AliasAs("code")]
        public ApiResultOfCMSceneInfoOfCode Code { get; set; }
        /// <summary>相关提示信息</summary>
        [AliasAs("msg")]
        [Required(AllowEmptyStrings = true)]
        public string Msg { get; set; }
        /// <summary>业务数据</summary>
        [AliasAs("data")]
        [Required]
        public System.Collections.Generic.List<CMSceneInfo> Data { get; set; } = new System.Collections.Generic.List<CMSceneInfo>();
    }
    /// <summary>表示Api结果</summary>
    public partial class ApiResultOfCMSmartLockBodyOf
    {
        /// <summary>错误码</summary>
        [AliasAs("code")]
        public ApiResultOfCMSmartLockBodyOfCode Code { get; set; }
        /// <summary>相关提示信息</summary>
        [AliasAs("msg")]
        [Required(AllowEmptyStrings = true)]
        public string Msg { get; set; }
        /// <summary>业务数据</summary>
        [AliasAs("data")]
        [Required]
        public System.Collections.Generic.List<CMSmartLockBody> Data { get; set; } = new System.Collections.Generic.List<CMSmartLockBody>();
    }
    /// <summary>智能门锁-临时密码管理</summary>
    public partial class CMTempPassWordSetting2
    {
        /// <summary>生效时间类型</summary>
        [AliasAs("effectMode")]
        public CMTempPassWordSetting2EffectMode? EffectMode { get; set; }
        /// <summary>经过指定秒数后生效</summary>
        [AliasAs("effectDelay")]
        public int? EffectDelay { get; set; }
        /// <summary>操作类型</summary>
        [AliasAs("actionType")]
        public CMTempPassWordSetting2ActionType? ActionType { get; set; }
        /// <summary>密码ID</summary>
        [AliasAs("pwdID")]
        public string PwdID { get; set; }
        /// <summary>临时密码(不可重复,如重复不处理)</summary>
        [AliasAs("tempPWD")]
        public string TempPWD { get; set; }
        /// <summary>在指定的时间点生效
        /// (年月日 时分)</summary>
        [AliasAs("effectTime")]
        public System.DateTime? EffectTime { get; set; }
        /// <summary>有效时长(小时 0.5,1,2,3,4 小时)</summary>
        [AliasAs("effectiveTime")]
        public double? EffectiveTime { get; set; }
        /// <summary>有效次数(1-254)</summary>
        [AliasAs("effectiveNum")]
        public int? EffectiveNum { get; set; }
        /// <summary>临时密码状态</summary>
        [AliasAs("state")]
        public CMTempPassWordSetting2State State { get; set; }
        /// <summary>超时的秒数
        /// 如果在Timeout秒之后才收到数据则不处理</summary>
        [AliasAs("timeout")]
        public int? Timeout { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("id")]
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
    }
    /// <summary>表示Api结果</summary>
    public partial class ApiResultOfCMTempPassWordBodyOf
    {
        /// <summary>错误码</summary>
        [AliasAs("code")]
        public ApiResultOfCMTempPassWordBodyOfCode Code { get; set; }
        /// <summary>相关提示信息</summary>
        [AliasAs("msg")]
        [Required(AllowEmptyStrings = true)]
        public string Msg { get; set; }
        /// <summary>业务数据</summary>
        [AliasAs("data")]
        [Required]
        public System.Collections.Generic.List<CMTempPassWordBody> Data { get; set; } = new System.Collections.Generic.List<CMTempPassWordBody>();
    }
    /// <summary>表示Api结果</summary>
    public partial class ApiResultOfCMInfAirKeyStateOf
    {
        /// <summary>错误码</summary>
        [AliasAs("code")]
        public ApiResultOfCMInfAirKeyStateOfCode Code { get; set; }
        /// <summary>相关提示信息</summary>
        [AliasAs("msg")]
        [Required(AllowEmptyStrings = true)]
        public string Msg { get; set; }
        /// <summary>业务数据</summary>
        [AliasAs("data")]
        [Required]
        public System.Collections.Generic.List<CMInfAirKeyState> Data { get; set; } = new System.Collections.Generic.List<CMInfAirKeyState>();
    }
    /// <summary>表示Api结果</summary>
    public partial class ApiResultOfCMInfTvKeyStateOf
    {
        /// <summary>错误码</summary>
        [AliasAs("code")]
        public ApiResultOfCMInfTvKeyStateOfCode Code { get; set; }
        /// <summary>相关提示信息</summary>
        [AliasAs("msg")]
        [Required(AllowEmptyStrings = true)]
        public string Msg { get; set; }
        /// <summary>业务数据</summary>
        [AliasAs("data")]
        [Required]
        public System.Collections.Generic.List<CMInfTvKeyState> Data { get; set; } = new System.Collections.Generic.List<CMInfTvKeyState>();
    }
    /// <summary>表示Api结果</summary>
    public partial class ApiResultOfCMInfTopboxKeyStateOf
    {
        /// <summary>错误码</summary>
        [AliasAs("code")]
        public ApiResultOfCMInfTopboxKeyStateOfCode Code { get; set; }
        /// <summary>相关提示信息</summary>
        [AliasAs("msg")]
        [Required(AllowEmptyStrings = true)]
        public string Msg { get; set; }
        /// <summary>业务数据</summary>
        [AliasAs("data")]
        [Required]
        public System.Collections.Generic.List<CMInfTopboxKeyState> Data { get; set; } = new System.Collections.Generic.List<CMInfTopboxKeyState>();
    }
    /// <summary>表示Api结果</summary>
    public partial class ApiResultOfCMInfDvdKeyStateOf
    {
        /// <summary>错误码</summary>
        [AliasAs("code")]
        public ApiResultOfCMInfDvdKeyStateOfCode Code { get; set; }
        /// <summary>相关提示信息</summary>
        [AliasAs("msg")]
        [Required(AllowEmptyStrings = true)]
        public string Msg { get; set; }
        /// <summary>业务数据</summary>
        [AliasAs("data")]
        [Required]
        public System.Collections.Generic.List<CMInfDvdKeyState> Data { get; set; } = new System.Collections.Generic.List<CMInfDvdKeyState>();
    }
    /// <summary>表示Api结果</summary>
    public partial class ApiResultOfCMInfTeleOneKeyStateOf
    {
        /// <summary>错误码</summary>
        [AliasAs("code")]
        public ApiResultOfCMInfTeleOneKeyStateOfCode Code { get; set; }
        /// <summary>相关提示信息</summary>
        [AliasAs("msg")]
        [Required(AllowEmptyStrings = true)]
        public string Msg { get; set; }
        /// <summary>业务数据</summary>
        [AliasAs("data")]
        [Required]
        public System.Collections.Generic.List<CMInfTeleOneKeyState> Data { get; set; } = new System.Collections.Generic.List<CMInfTeleOneKeyState>();
    }
    /// <summary>表示Api结果</summary>
    public partial class ApiResultOfCMInfTeleTwoKeyStateOf
    {
        /// <summary>错误码</summary>
        [AliasAs("code")]
        public ApiResultOfCMInfTeleTwoKeyStateOfCode Code { get; set; }
        /// <summary>相关提示信息</summary>
        [AliasAs("msg")]
        [Required(AllowEmptyStrings = true)]
        public string Msg { get; set; }
        /// <summary>业务数据</summary>
        [AliasAs("data")]
        [Required]
        public System.Collections.Generic.List<CMInfTeleTwoKeyState> Data { get; set; } = new System.Collections.Generic.List<CMInfTeleTwoKeyState>();
    }
    /// <summary>表示Api结果</summary>
    public partial class ApiResultOfInt16
    {
        /// <summary>错误码</summary>
        [AliasAs("code")]
        public ApiResultOfInt16Code Code { get; set; }
        /// <summary>相关提示信息</summary>
        [AliasAs("msg")]
        [Required(AllowEmptyStrings = true)]
        public string Msg { get; set; }
        /// <summary>业务数据</summary>
        [AliasAs("data")]
        public int Data { get; set; }
    }
    /// <summary>表示MQTT的消息</summary>
    public partial class MessageOfEmptyInfo
    {
        /// <summary>Api</summary>
        [AliasAs("api")]
        public MessageOfEmptyInfoApi? Api { get; set; }
        /// <summary>时间unix时间戳(s)</summary>
        [AliasAs("time")]
        public int? Time { get; set; }
        /// <summary>模式</summary>
        [AliasAs("mode")]
        public MessageOfEmptyInfoMode? Mode { get; set; }
        /// <summary>消息的唯一标识符</summary>
        [AliasAs("id")]
        public int? Id { get; set; }
        /// <summary>内容体</summary>
        [AliasAs("body")]
        public object Body { get; set; }
    }
    /// <summary>表示批量文本内容</summary>
    public partial class StringDataBat
    {
        /// <summary>设备机身号，支持多个，使用半角逗号间隔</summary>
        [AliasAs("nums")]
        [Required(AllowEmptyStrings = true)]
        public string Nums { get; set; }
        /// <summary>数据内容</summary>
        [AliasAs("data")]
        [Required(AllowEmptyStrings = true)]
        public string Data { get; set; }
    }
    /// <summary>表示Api结果</summary>
    public partial class ApiResultOfStringOf
    {
        /// <summary>错误码</summary>
        [AliasAs("code")]
        public ApiResultOfStringOfCode Code { get; set; }
        /// <summary>相关提示信息</summary>
        [AliasAs("msg")]
        [Required(AllowEmptyStrings = true)]
        public string Msg { get; set; }
        /// <summary>业务数据</summary>
        [AliasAs("data")]
        [Required]
        public System.Collections.Generic.List<string> Data { get; set; } = new System.Collections.Generic.List<string>();
    }
    /// <summary>表示Api结果</summary>
    public partial class ApiResultOfIntercomAccount_Dto
    {
        /// <summary>错误码</summary>
        [AliasAs("code")]
        public ApiResultOfIntercomAccount_DtoCode Code { get; set; }
        /// <summary>相关提示信息</summary>
        [AliasAs("msg")]
        [Required(AllowEmptyStrings = true)]
        public string Msg { get; set; }
        /// <summary>业务数据</summary>
        [AliasAs("data")]
        [Required]
        public IntercomAccount_Dto Data { get; set; } = new IntercomAccount_Dto();
    }
    /// <summary>对讲账号</summary>
    public partial class IntercomAccount_Dto
    {
        /// <summary>是否新创建
        /// 如果对讲的UserId已创建过，值为false</summary>
        [AliasAs("isNewCreate")]
        public bool? IsNewCreate { get; set; }
        /// <summary>对讲Id
        /// 其它设备通过这个对讲id与本设备进行对讲</summary>
        [AliasAs("userId")]
        public string UserId { get; set; }
        /// <summary>登录Token
        /// 对讲功能初始化需要</summary>
        [AliasAs("loginToken")]
        public string LoginToken { get; set; }
        /// <summary>创建时间</summary>
        [AliasAs("createTime")]
        public System.DateTime? CreateTime { get; set; }
    }
    /// <summary>表示Api结果</summary>
    public partial class ApiResultOfLauncher
    {
        /// <summary>错误码</summary>
        [AliasAs("code")]
        public ApiResultOfLauncherCode Code { get; set; }
        /// <summary>相关提示信息</summary>
        [AliasAs("msg")]
        [Required(AllowEmptyStrings = true)]
        public string Msg { get; set; }
        /// <summary>业务数据</summary>
        [AliasAs("data")]
        [Required]
        public Launcher Data { get; set; } = new Launcher();
    }
    /// <summary>启动器</summary>
    public partial class Launcher
    {
        /// <summary>ID</summary>
        [AliasAs("id")]
        [StringLength(50)]
        public string Id { get; set; }
        /// <summary>主版本</summary>
        [AliasAs("versionCode")]
        public int VersionCode { get; set; }
        /// <summary>版本名</summary>
        [AliasAs("versionName")]
        [Required(AllowEmptyStrings = true)]
        [StringLength(50)]
        public string VersionName { get; set; }
        /// <summary>包名</summary>
        [AliasAs("packageName")]
        [Required(AllowEmptyStrings = true)]
        [StringLength(50)]
        public string PackageName { get; set; }
        /// <summary>文件Md5</summary>
        [AliasAs("fileMd5")]
        [Required(AllowEmptyStrings = true)]
        [StringLength(50)]
        public string FileMd5 { get; set; }
        /// <summary>文件路径</summary>
        [AliasAs("fileURL")]
        [Required(AllowEmptyStrings = true)]
        [StringLength(256)]
        public string FileURL { get; set; }
        /// <summary>创建时间</summary>
        [AliasAs("createTime")]
        public System.DateTime? CreateTime { get; set; }
    }
    /// <summary>表示Api结果</summary>
    public partial class ApiResultOfSignedApkOf
    {
        /// <summary>错误码</summary>
        [AliasAs("code")]
        public ApiResultOfSignedApkOfCode Code { get; set; }
        /// <summary>相关提示信息</summary>
        [AliasAs("msg")]
        [Required(AllowEmptyStrings = true)]
        public string Msg { get; set; }
        /// <summary>业务数据</summary>
        [AliasAs("data")]
        [Required]
        public System.Collections.Generic.List<SignedApk> Data { get; set; } = new System.Collections.Generic.List<SignedApk>();
    }
    /// <summary>已签名的Apk</summary>
    public partial class SignedApk
    {
        /// <summary>获取或设置唯一标识</summary>
        [AliasAs("id")]
        [StringLength(50)]
        public string Id { get; set; }
        /// <summary>关联的开发者id</summary>
        [AliasAs("dL_Id")]
        [Required(AllowEmptyStrings = true)]
        [StringLength(50)]
        public string DL_Id { get; set; }
        /// <summary>Apk名称</summary>
        [AliasAs("apkName")]
        [StringLength(250)]
        public string ApkName { get; set; }
        /// <summary>签名后Apk的URL</summary>
        [AliasAs("apkURL")]
        [StringLength(250)]
        public string ApkURL { get; set; }
        /// <summary>创建时间</summary>
        [AliasAs("createTime")]
        public System.DateTime? CreateTime { get; set; }
    }
    /// <summary>表示Api结果</summary>
    public partial class ApiResultOfMqttService
    {
        /// <summary>错误码</summary>
        [AliasAs("code")]
        public ApiResultOfMqttServiceCode Code { get; set; }
        /// <summary>相关提示信息</summary>
        [AliasAs("msg")]
        [Required(AllowEmptyStrings = true)]
        public string Msg { get; set; }
        /// <summary>业务数据</summary>
        [AliasAs("data")]
        [Required]
        public MqttService Data { get; set; } = new MqttService();
    }
    /// <summary>Mqtt服务信息</summary>
    public partial class MqttService
    {
        /// <summary>服务器ip</summary>
        [AliasAs("ip")]
        public string Ip { get; set; }
        /// <summary>服务器端口</summary>
        [AliasAs("port")]
        public int? Port { get; set; }
        /// <summary>设备可发布的主题</summary>
        [AliasAs("pubTopic")]
        public string PubTopic { get; set; }
        /// <summary>设备可订阅的主题</summary>
        [AliasAs("subTopics")]
        public System.Collections.Generic.List<string> SubTopics { get; set; }
        /// <summary>时间同步服务器地址</summary>
        [AliasAs("ntpServer")]
        public string NtpServer { get; set; }
        /// <summary>业务平台URL</summary>
        [AliasAs("platformURL")]
        public string PlatformURL { get; set; }
    }
    /// <summary>设备状态通知请求</summary>
    public partial class DeviceStateNotify
    {
        /// <summary>应用的Id</summary>
        [AliasAs("applicationId")]
        public string ApplicationId { get; set; }
        /// <summary>设备机身号</summary>
        [AliasAs("num")]
        public string Num { get; set; }
        /// <summary>状态变化的时间</summary>
        [AliasAs("createTime")]
        public System.DateTime? CreateTime { get; set; }
        /// <summary>设备是否在线</summary>
        [AliasAs("isOnline")]
        public bool? IsOnline { get; set; }
    }
    /// <summary>呼叫挂机计费回调记录</summary>
    public partial class CallhangupNotify
    {
        /// <summary>唯一Id</summary>
        [AliasAs("id")]
        public string Id { get; set; }
        /// <summary>应用的Id</summary>
        [AliasAs("applicationId")]
        public string ApplicationId { get; set; }
        /// <summary>呼叫费用类别</summary>
        [AliasAs("callType")]
        public CallhangupNotifyCallType? CallType { get; set; }
        /// <summary>被叫号码类型，0：client账号，1：普通电话，2：userid</summary>
        [AliasAs("calledType")]
        public CallhangupNotifyCalledType? CalledType { get; set; }
        /// <summary>主叫号码，主叫的对讲账号</summary>
        [AliasAs("caller")]
        public string Caller { get; set; }
        /// <summary>被叫号码</summary>
        [AliasAs("called")]
        public string Called { get; set; }
        /// <summary>开始通话时间</summary>
        [AliasAs("startTime")]
        public System.DateTime? StartTime { get; set; }
        /// <summary>结束通话时间</summary>
        [AliasAs("stopTime")]
        public System.DateTime? StopTime { get; set; }
        /// <summary>通话时长(s)</summary>
        [AliasAs("length")]
        public int? Length { get; set; }
        /// <summary>挂机原因描述</summary>
        [AliasAs("reason")]
        public CallhangupNotifyReason? Reason { get; set; }
        /// <summary>计费(分钱）</summary>
        [AliasAs("cost")]
        public int? Cost { get; set; }
        /// <summary>创建时间</summary>
        [AliasAs("createTime")]
        public System.DateTime? CreateTime { get; set; }
    }
    /// <summary>中控机设备报警通知</summary>
    public partial class CMAlarmNotify
    {
        /// <summary>关联的应用Id</summary>
        [AliasAs("applicationId")]
        public string ApplicationId { get; set; }
        /// <summary>中控机设备机身号</summary>
        [AliasAs("num")]
        public string Num { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("subId")]
        public string SubId { get; set; }
        /// <summary>中控机的设备类型</summary>
        [AliasAs("subType")]
        public int? SubType { get; set; }
        /// <summary>报警时间</summary>
        [AliasAs("createTime")]
        public System.DateTime? CreateTime { get; set; }
        /// <summary>报警内容</summary>
        [AliasAs("alarmContent")]
        public string AlarmContent { get; set; }
        /// <summary>报警类型</summary>
        [AliasAs("alarmType")]
        public CMAlarmNotifyAlarmType? AlarmType { get; set; }
    }
    /// <summary>智能家居-开锁日志上报</summary>
    public partial class CmOpenRecordNotify
    {
        /// <summary>关联的应用Id</summary>
        [AliasAs("applicationId")]
        public string ApplicationId { get; set; }
        /// <summary>中控机设备机身号</summary>
        [AliasAs("num")]
        public string Num { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("subId")]
        public string SubId { get; set; }
        /// <summary>钥匙标签</summary>
        [AliasAs("label")]
        public string Label { get; set; }
        /// <summary>开门类型</summary>
        [AliasAs("openType")]
        public CmOpenRecordNotifyOpenType? OpenType { get; set; }
        /// <summary>开门时间</summary>
        [AliasAs("createTime")]
        public System.DateTime? CreateTime { get; set; }
    }
    /// <summary>环境检测仪通知</summary>
    public partial class CmEnvironDetectorNotify
    {
        /// <summary>关联的应用Id</summary>
        [AliasAs("applicationId")]
        public string ApplicationId { get; set; }
        /// <summary>中控机设备机身号</summary>
        [AliasAs("num")]
        public string Num { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("subId")]
        public string SubId { get; set; }
        /// <summary>中控机的设备类型(58)</summary>
        [AliasAs("subType")]
        public int? SubType { get; set; }
        /// <summary>温度 单位 ℃</summary>
        [AliasAs("temperature")]
        public int? Temperature { get; set; }
        /// <summary>湿度 单位%</summary>
        [AliasAs("humidity")]
        public int? Humidity { get; set; }
        /// <summary>甲醛 mg/m3 浮点（两位小数）</summary>
        [AliasAs("formaldehyde")]
        public double? Formaldehyde { get; set; }
        /// <summary>PM2.5 单位μg/m3</summary>
        [AliasAs("pM25")]
        public int? PM25 { get; set; }
        /// <summary>TVOC mg/m3 浮点（两位小数）</summary>
        [AliasAs("tvoc")]
        public double? Tvoc { get; set; }
        /// <summary>上报时间</summary>
        [AliasAs("createTime")]
        public System.DateTime? CreateTime { get; set; }
    }
    /// <summary>10A/16A 计量插座 - 设备每隔30分钟上报功率</summary>
    public partial class CmNotifyHubPower
    {
        /// <summary>功率</summary>
        [AliasAs("value")]
        public int? Value { get; set; }
        /// <summary>关联的应用Id</summary>
        [AliasAs("applicationId")]
        public string ApplicationId { get; set; }
        /// <summary>中控机设备机身号</summary>
        [AliasAs("num")]
        public string Num { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("subId")]
        public string SubId { get; set; }
        /// <summary>中控机的设备类型(10A_47, 16A_48)</summary>
        [AliasAs("subType")]
        public int? SubType { get; set; }
        /// <summary>上报时间</summary>
        [AliasAs("createTime")]
        public System.DateTime? CreateTime { get; set; }
    }
    /// <summary>10A/16A 计量插座 - 设备每隔30分钟上报用电量</summary>
    public partial class CmNotifyHubWh
    {
        /// <summary>用电量</summary>
        [AliasAs("value")]
        public double? Value { get; set; }
        /// <summary>关联的应用Id</summary>
        [AliasAs("applicationId")]
        public string ApplicationId { get; set; }
        /// <summary>中控机设备机身号</summary>
        [AliasAs("num")]
        public string Num { get; set; }
        /// <summary>中控机的设备id</summary>
        [AliasAs("subId")]
        public string SubId { get; set; }
        /// <summary>中控机的设备类型(10A_47, 16A_48)</summary>
        [AliasAs("subType")]
        public int? SubType { get; set; }
        /// <summary>上报时间</summary>
        [AliasAs("createTime")]
        public System.DateTime? CreateTime { get; set; }
    }
    /// <summary>表示Api结果</summary>
    public partial class ApiResultOfAppInfoOf
    {
        /// <summary>错误码</summary>
        [AliasAs("code")]
        public ApiResultOfAppInfoOfCode Code { get; set; }
        /// <summary>相关提示信息</summary>
        [AliasAs("msg")]
        [Required(AllowEmptyStrings = true)]
        public string Msg { get; set; }
        /// <summary>业务数据</summary>
        [AliasAs("data")]
        [Required]
        public System.Collections.Generic.List<AppInfo> Data { get; set; } = new System.Collections.Generic.List<AppInfo>();
    }
    /// <summary>Application信息</summary>
    public partial class AppInfo
    {
        /// <summary>应用id</summary>
        [AliasAs("id")]
        public string Id { get; set; }
        /// <summary>应用token</summary>
        [AliasAs("token")]
        public string Token { get; set; }
        /// <summary>应用名称</summary>
        [AliasAs("name")]
        public string Name { get; set; }
        /// <summary>关联的开发者账号</summary>
        [AliasAs("developAccount")]
        public string DevelopAccount { get; set; }
    }
    /// <summary>表示Api结果</summary>
    public partial class ApiResultOfAppInfoGroupOf
    {
        /// <summary>错误码</summary>
        [AliasAs("code")]
        public ApiResultOfAppInfoGroupOfCode Code { get; set; }
        /// <summary>相关提示信息</summary>
        [AliasAs("msg")]
        [Required(AllowEmptyStrings = true)]
        public string Msg { get; set; }
        /// <summary>业务数据</summary>
        [AliasAs("data")]
        [Required]
        public System.Collections.Generic.List<AppInfoGroup> Data { get; set; } = new System.Collections.Generic.List<AppInfoGroup>();
    }
    /// <summary>Application信息分组</summary>
    public partial class AppInfoGroup
    {
        /// <summary>开发者账号</summary>
        [AliasAs("developAccount")]
        public string DevelopAccount { get; set; }
        /// <summary>开发者下的应用</summary>
        [AliasAs("appInfos")]
        public System.Collections.Generic.List<AppInfo> AppInfos { get; set; }
    }
    /// <summary>表示Api结果</summary>
    public partial class ApiResultOfPushAccount_Dto
    {
        /// <summary>错误码</summary>
        [AliasAs("code")]
        public ApiResultOfPushAccount_DtoCode Code { get; set; }
        /// <summary>相关提示信息</summary>
        [AliasAs("msg")]
        [Required(AllowEmptyStrings = true)]
        public string Msg { get; set; }
        /// <summary>业务数据</summary>
        [AliasAs("data")]
        [Required]
        public PushAccount_Dto Data { get; set; } = new PushAccount_Dto();
    }
    /// <summary>推送账号</summary>
    public partial class PushAccount_Dto
    {
        /// <summary>推送ID</summary>
        [AliasAs("id")]
        public string Id { get; set; }
        /// <summary>token</summary>
        [AliasAs("token")]
        public string Token { get; set; }
        /// <summary>创建时间</summary>
        [AliasAs("createTime")]
        public System.DateTime? CreateTime { get; set; }
    }
    /// <summary>表示批量文本内容</summary>
    public partial class StringDataPushBat
    {
        /// <summary>推送id，支持多个，使用半角逗号间隔</summary>
        [AliasAs("ids")]
        [Required(AllowEmptyStrings = true)]
        public string Ids { get; set; }
        /// <summary>数据内容</summary>
        [AliasAs("data")]
        [Required(AllowEmptyStrings = true)]
        public string Data { get; set; }
    }
    public enum ApiResultOfStringCode
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
    }
    public enum SysCtrlSettingCtrlCode
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
    }
    public enum ApiResultOfBooleanCode
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
    }
    public enum ApiResultOfDeviceInfoCode
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
    }
    public enum DeviceInfoProductType
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
        _8 = 8,
    }
    public enum ApiResultOfPageOfDeviceInfoCode
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
    }
    public enum MessageOfTimeoutRequestApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfTimeoutRequestMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum MessageOfCMDeivceDataApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMDeivceDataMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMDeviceInfoType
    {
        _6 = 6,
        _7 = 7,
        _8 = 8,
        _9 = 9,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
        _14 = 14,
        _15 = 15,
        _16 = 16,
        _17 = 17,
        _18 = 18,
        _19 = 19,
        _20 = 20,
        _21 = 21,
        _22 = 22,
        _23 = 23,
        _25 = 25,
        _26 = 26,
        _27 = 27,
        _28 = 28,
        _29 = 29,
        _30 = 30,
        _31 = 31,
        _32 = 32,
        _35 = 35,
        _36 = 36,
        _37 = 37,
        _38 = 38,
        _39 = 39,
        _41 = 41,
        _42 = 42,
        _43 = 43,
        _44 = 44,
        _45 = 45,
        _46 = 46,
        _47 = 47,
        _48 = 48,
        _49 = 49,
        _52 = 52,
        _53 = 53,
        _54 = 54,
        _55 = 55,
        _56 = 56,
        _58 = 58,
    }
    public enum CMDeviceInfoSwitchState
    {
        _0 = 0,
        _1 = 1,
    }
    public enum CMDeviceInfoCoreParamType
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _5 = 5,
        _6 = 6,
        _7 = 7,
        _8 = 8,
    }
    public enum MessageOfCMSceneInfoOfApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMSceneInfoOfMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMSceneInfoMode
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
    }
    public enum MessageOfCMSceneSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMSceneSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum MessageOfBooleanApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfBooleanMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum MessageOfCMAlarmInfoApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMAlarmInfoMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMAlarmInfoType
    {
        _6 = 6,
        _7 = 7,
        _8 = 8,
        _9 = 9,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
        _14 = 14,
        _15 = 15,
        _16 = 16,
        _17 = 17,
        _18 = 18,
        _19 = 19,
        _20 = 20,
        _21 = 21,
        _22 = 22,
        _23 = 23,
        _25 = 25,
        _26 = 26,
        _27 = 27,
        _28 = 28,
        _29 = 29,
        _30 = 30,
        _31 = 31,
        _32 = 32,
        _35 = 35,
        _36 = 36,
        _37 = 37,
        _38 = 38,
        _39 = 39,
        _41 = 41,
        _42 = 42,
        _43 = 43,
        _44 = 44,
        _45 = 45,
        _46 = 46,
        _47 = 47,
        _48 = 48,
        _49 = 49,
        _52 = 52,
        _53 = 53,
        _54 = 54,
        _55 = 55,
        _56 = 56,
        _58 = 58,
    }
    public enum CMAlarmInfoAlertType
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _5 = 5,
        _6 = 6,
        _7 = 7,
        _8 = 8,
        _9 = 9,
        _10 = 10,
    }
    public enum MessageOfCMOpenRecordInfoApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMOpenRecordInfoMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMOpenRecordInfoType
    {
        _6 = 6,
        _7 = 7,
        _8 = 8,
        _9 = 9,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
        _14 = 14,
        _15 = 15,
        _16 = 16,
        _17 = 17,
        _18 = 18,
        _19 = 19,
        _20 = 20,
        _21 = 21,
        _22 = 22,
        _23 = 23,
        _25 = 25,
        _26 = 26,
        _27 = 27,
        _28 = 28,
        _29 = 29,
        _30 = 30,
        _31 = 31,
        _32 = 32,
        _35 = 35,
        _36 = 36,
        _37 = 37,
        _38 = 38,
        _39 = 39,
        _41 = 41,
        _42 = 42,
        _43 = 43,
        _44 = 44,
        _45 = 45,
        _46 = 46,
        _47 = 47,
        _48 = 48,
        _49 = 49,
        _52 = 52,
        _53 = 53,
        _54 = 54,
        _55 = 55,
        _56 = 56,
        _58 = 58,
    }
    public enum CMOpenRecordInfoOpenType
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
    }
    public enum MessageOfCMLedSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMLedSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMLedSettingLedEnums
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _5 = 5,
        _6 = 6,
        _7 = 7,
        _8 = 8,
        _9 = 9,
        _10 = 10,
        _11 = 11,
        _12 = 12,
    }
    public enum MessageOfCMCustomTwoSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMCustomTwoSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMCustomTwoSettingCustomTwo
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _5 = 5,
        _6 = 6,
        _7 = 7,
        _8 = 8,
        _9 = 9,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _101 = 101,
        _102 = 102,
        _103 = 103,
        _104 = 104,
        _105 = 105,
        _106 = 106,
        _107 = 107,
        _108 = 108,
        _109 = 109,
        _110 = 110,
        _111 = 111,
        _112 = 112,
    }
    public enum MessageOfCMLightControlSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMLightControlSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMLightControlSettingLightSwitch
    {
        _0 = 0,
        _1 = 1,
    }
    public enum MessageOfCMDimmerSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMDimmerSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMDimmerSettingBrightness
    {
        _0 = 0,
        _20 = 20,
        _40 = 40,
        _60 = 60,
        _80 = 80,
        _100 = 100,
    }
    public enum MessageOfCMAirSwitchSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMAirSwitchSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMAirSwitchSettingAirSwitch
    {
        _0 = 0,
        _1 = 1,
        _16 = 16,
        _17 = 17,
        _18 = 18,
        _19 = 19,
        _20 = 20,
        _21 = 21,
        _22 = 22,
        _23 = 23,
        _24 = 24,
        _25 = 25,
        _26 = 26,
        _27 = 27,
        _28 = 28,
        _29 = 29,
        _30 = 30,
        _116 = 116,
        _117 = 117,
        _118 = 118,
        _119 = 119,
        _120 = 120,
        _121 = 121,
        _122 = 122,
        _123 = 123,
        _124 = 124,
        _125 = 125,
        _126 = 126,
        _127 = 127,
        _128 = 128,
        _129 = 129,
        _130 = 130,
    }
    public enum MessageOfCMOutletSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMOutletSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMOutletSettingOutletSwitch
    {
        _0 = 0,
        _1 = 1,
    }
    public enum MessageOfCMCurtainSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMCurtainSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMCurtainSettingSchedule
    {
        _0 = 0,
        _20 = 20,
        _40 = 40,
        _60 = 60,
        _80 = 80,
        _100 = 100,
    }
    public enum MessageOfCMSecuritySettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMSecuritySettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMSecuritySettingState
    {
        _0 = 0,
        _1 = 1,
    }
    public enum MessageOfCMDoorbellSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMDoorbellSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum MessageOfCMSingleLightSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMSingleLightSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMSingleLightSettingIsOpen
    {
        _0 = 0,
        _1 = 1,
    }
    public enum MessageOfCMCentralAirSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMCentralAirSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMCentralAirSettingCentralAir
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _5 = 5,
        _6 = 6,
        _16 = 16,
        _17 = 17,
        _18 = 18,
        _19 = 19,
        _20 = 20,
        _21 = 21,
        _22 = 22,
        _23 = 23,
        _24 = 24,
        _25 = 25,
        _26 = 26,
        _27 = 27,
        _28 = 28,
        _29 = 29,
        _30 = 30,
        _116 = 116,
        _117 = 117,
        _118 = 118,
        _119 = 119,
        _120 = 120,
        _121 = 121,
        _122 = 122,
        _123 = 123,
        _124 = 124,
        _125 = 125,
        _126 = 126,
        _127 = 127,
        _128 = 128,
        _129 = 129,
        _130 = 130,
    }
    public enum MessageOfCMFreshAirSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMFreshAirSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMFreshAirSettingFreshAir
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
    }
    public enum MessageOfCMTouchLightSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMTouchLightSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMTouchLightSettingLightSwitch
    {
        _0 = 0,
        _1 = 1,
    }
    public enum MessageOfCMCtrlRequestApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMCtrlRequestMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum MessageOfCMSmartLockSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMSmartLockSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMSmartLockSettingActionType
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
    }
    public enum CMSmartLockSettingKeyType
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
    }
    public enum MessageOfCMSmartLockBodyOfApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMSmartLockBodyOfMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMSmartLockBodyKeyType
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
    }
    public enum MessageOfCMTempPassWordSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMTempPassWordSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMTempPassWordSettingActionType
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
    }
    public enum CMTempPassWordSettingState
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
    }
    public enum MessageOfCMTempPassWordBodyOfApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMTempPassWordBodyOfMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMTempPassWordBodyState
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
    }
    public enum MessageOfCMBLFreshAirSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMBLFreshAirSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMBLFreshAirSettingBlFreshAir
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _5 = 5,
        _6 = 6,
        _7 = 7,
        _8 = 8,
        _9 = 9,
    }
    public enum MessageOfCMFanSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMFanSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum MessageOfCMImmunitySettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMImmunitySettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMImmunitySettingImmunityEnums
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
    }
    public enum MessageOfCMLampMasterSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMLampMasterSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMLampMasterSettingLampSwitch
    {
        _0 = 0,
        _1 = 1,
    }
    public enum MessageOfCMInfAirKeyStateOfApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMInfAirKeyStateOfMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMInfAirKeyStateKey
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _5 = 5,
        _6 = 6,
        _7 = 7,
        _8 = 8,
        _9 = 9,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
        _14 = 14,
        _15 = 15,
        _16 = 16,
        _17 = 17,
        _18 = 18,
        _19 = 19,
        _20 = 20,
        _21 = 21,
        _22 = 22,
        _23 = 23,
        _24 = 24,
        _25 = 25,
        _26 = 26,
        _27 = 27,
        _28 = 28,
        _29 = 29,
        _30 = 30,
        _31 = 31,
        _32 = 32,
        _33 = 33,
        _34 = 34,
        _35 = 35,
    }
    public enum MessageOfCMInfAirKeyCtrlApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMInfAirKeyCtrlMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMInfAirKeyCtrlKey
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _5 = 5,
        _6 = 6,
        _7 = 7,
        _8 = 8,
        _9 = 9,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
        _14 = 14,
        _15 = 15,
        _16 = 16,
        _17 = 17,
        _18 = 18,
        _19 = 19,
        _20 = 20,
        _21 = 21,
        _22 = 22,
        _23 = 23,
        _24 = 24,
        _25 = 25,
        _26 = 26,
        _27 = 27,
        _28 = 28,
        _29 = 29,
        _30 = 30,
        _31 = 31,
        _32 = 32,
        _33 = 33,
        _34 = 34,
        _35 = 35,
    }
    public enum MessageOfCMInfTvKeyStateOfApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMInfTvKeyStateOfMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMInfTvKeyStateKey
    {
        _36 = 36,
        _37 = 37,
        _38 = 38,
        _39 = 39,
        _40 = 40,
        _41 = 41,
        _42 = 42,
        _43 = 43,
        _44 = 44,
        _45 = 45,
        _46 = 46,
        _47 = 47,
        _48 = 48,
        _49 = 49,
        _50 = 50,
        _51 = 51,
        _52 = 52,
        _53 = 53,
        _54 = 54,
        _55 = 55,
        _56 = 56,
        _57 = 57,
        _58 = 58,
        _59 = 59,
        _60 = 60,
        _61 = 61,
    }
    public enum MessageOfCMInfTvKeyCtrlApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMInfTvKeyCtrlMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMInfTvKeyCtrlKey
    {
        _36 = 36,
        _37 = 37,
        _38 = 38,
        _39 = 39,
        _40 = 40,
        _41 = 41,
        _42 = 42,
        _43 = 43,
        _44 = 44,
        _45 = 45,
        _46 = 46,
        _47 = 47,
        _48 = 48,
        _49 = 49,
        _50 = 50,
        _51 = 51,
        _52 = 52,
        _53 = 53,
        _54 = 54,
        _55 = 55,
        _56 = 56,
        _57 = 57,
        _58 = 58,
        _59 = 59,
        _60 = 60,
        _61 = 61,
    }
    public enum MessageOfCMInfTopboxKeyStateOfApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMInfTopboxKeyStateOfMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMInfTopboxKeyStateKey
    {
        _62 = 62,
        _63 = 63,
        _64 = 64,
        _65 = 65,
        _66 = 66,
        _67 = 67,
        _68 = 68,
        _69 = 69,
        _70 = 70,
        _71 = 71,
        _72 = 72,
        _73 = 73,
        _74 = 74,
        _75 = 75,
        _76 = 76,
        _77 = 77,
        _78 = 78,
        _79 = 79,
        _80 = 80,
        _81 = 81,
        _82 = 82,
        _83 = 83,
        _84 = 84,
        _85 = 85,
        _86 = 86,
        _87 = 87,
        _88 = 88,
        _89 = 89,
        _90 = 90,
    }
    public enum MessageOfCMInfTopboxKeyCtrlApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMInfTopboxKeyCtrlMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMInfTopboxKeyCtrlKey
    {
        _62 = 62,
        _63 = 63,
        _64 = 64,
        _65 = 65,
        _66 = 66,
        _67 = 67,
        _68 = 68,
        _69 = 69,
        _70 = 70,
        _71 = 71,
        _72 = 72,
        _73 = 73,
        _74 = 74,
        _75 = 75,
        _76 = 76,
        _77 = 77,
        _78 = 78,
        _79 = 79,
        _80 = 80,
        _81 = 81,
        _82 = 82,
        _83 = 83,
        _84 = 84,
        _85 = 85,
        _86 = 86,
        _87 = 87,
        _88 = 88,
        _89 = 89,
        _90 = 90,
    }
    public enum MessageOfCMInfDvdKeyStateOfApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMInfDvdKeyStateOfMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMInfDvdKeyStateKey
    {
        _91 = 91,
        _92 = 92,
        _93 = 93,
        _94 = 94,
        _95 = 95,
        _96 = 96,
        _97 = 97,
        _98 = 98,
        _99 = 99,
        _100 = 100,
        _101 = 101,
        _102 = 102,
        _103 = 103,
        _104 = 104,
        _105 = 105,
        _106 = 106,
        _107 = 107,
        _108 = 108,
        _109 = 109,
        _110 = 110,
        _111 = 111,
        _112 = 112,
        _113 = 113,
    }
    public enum MessageOfCMInfDvdKeyCtrlApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMInfDvdKeyCtrlMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMInfDvdKeyCtrlKey
    {
        _91 = 91,
        _92 = 92,
        _93 = 93,
        _94 = 94,
        _95 = 95,
        _96 = 96,
        _97 = 97,
        _98 = 98,
        _99 = 99,
        _100 = 100,
        _101 = 101,
        _102 = 102,
        _103 = 103,
        _104 = 104,
        _105 = 105,
        _106 = 106,
        _107 = 107,
        _108 = 108,
        _109 = 109,
        _110 = 110,
        _111 = 111,
        _112 = 112,
        _113 = 113,
    }
    public enum MessageOfCMInfTeleOneKeyStateOfApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMInfTeleOneKeyStateOfMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMInfTeleOneKeyStateKey
    {
        _114 = 114,
        _115 = 115,
        _116 = 116,
        _117 = 117,
        _118 = 118,
        _119 = 119,
        _120 = 120,
        _121 = 121,
        _122 = 122,
        _123 = 123,
        _124 = 124,
        _125 = 125,
        _126 = 126,
        _127 = 127,
        _128 = 128,
        _129 = 129,
        _130 = 130,
        _131 = 131,
        _132 = 132,
        _133 = 133,
        _134 = 134,
        _135 = 135,
        _136 = 136,
        _137 = 137,
    }
    public enum MessageOfCMInfTeleOneKeyCtrlApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMInfTeleOneKeyCtrlMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMInfTeleOneKeyCtrlKey
    {
        _114 = 114,
        _115 = 115,
        _116 = 116,
        _117 = 117,
        _118 = 118,
        _119 = 119,
        _120 = 120,
        _121 = 121,
        _122 = 122,
        _123 = 123,
        _124 = 124,
        _125 = 125,
        _126 = 126,
        _127 = 127,
        _128 = 128,
        _129 = 129,
        _130 = 130,
        _131 = 131,
        _132 = 132,
        _133 = 133,
        _134 = 134,
        _135 = 135,
        _136 = 136,
        _137 = 137,
    }
    public enum MessageOfCMInfTeleTwoKeyStateOfApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMInfTeleTwoKeyStateOfMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMInfTeleTwoKeyStateKey
    {
        _138 = 138,
        _139 = 139,
        _140 = 140,
        _141 = 141,
        _142 = 142,
        _143 = 143,
        _144 = 144,
        _145 = 145,
        _146 = 146,
        _147 = 147,
        _148 = 148,
        _149 = 149,
        _150 = 150,
        _151 = 151,
        _152 = 152,
        _153 = 153,
        _154 = 154,
        _155 = 155,
        _156 = 156,
        _157 = 157,
        _158 = 158,
        _159 = 159,
        _160 = 160,
        _161 = 161,
    }
    public enum MessageOfCMInfTeleTwoKeyCtrlApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMInfTeleTwoKeyCtrlMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMInfTeleTwoKeyCtrlKey
    {
        _138 = 138,
        _139 = 139,
        _140 = 140,
        _141 = 141,
        _142 = 142,
        _143 = 143,
        _144 = 144,
        _145 = 145,
        _146 = 146,
        _147 = 147,
        _148 = 148,
        _149 = 149,
        _150 = 150,
        _151 = 151,
        _152 = 152,
        _153 = 153,
        _154 = 154,
        _155 = 155,
        _156 = 156,
        _157 = 157,
        _158 = 158,
        _159 = 159,
        _160 = 160,
        _161 = 161,
    }
    public enum MessageOfCMDaiKinAirSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMDaiKinAirSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMDaiKinAirSettingDaiKinAir
    {
        _0 = 0,
        _1 = 1,
        _16 = 16,
        _17 = 17,
        _18 = 18,
        _19 = 19,
        _20 = 20,
        _21 = 21,
        _22 = 22,
        _23 = 23,
        _24 = 24,
        _25 = 25,
        _26 = 26,
        _27 = 27,
        _28 = 28,
        _29 = 29,
        _30 = 30,
        _31 = 31,
        _32 = 32,
        _33 = 33,
        _34 = 34,
        _35 = 35,
        _36 = 36,
        _37 = 37,
        _38 = 38,
        _39 = 39,
        _40 = 40,
        _41 = 41,
        _42 = 42,
        _43 = 43,
        _44 = 44,
        _45 = 45,
    }
    public enum MessageOfCMAirSwitch3SettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMAirSwitch3SettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMAirSwitch3SettingAirSwitch
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
    }
    public enum MessageOfCMGroundHeatingSwitchSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMGroundHeatingSwitchSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMGroundHeatingSwitchSettingSwitch
    {
        _0 = 0,
        _1 = 1,
    }
    public enum MessageOfCMZTEFanCoilSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMZTEFanCoilSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMZTEFanCoilSettingZteFanCoil
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _5 = 5,
        _16 = 16,
        _17 = 17,
        _18 = 18,
        _19 = 19,
        _20 = 20,
        _21 = 21,
        _22 = 22,
        _23 = 23,
        _24 = 24,
        _25 = 25,
        _26 = 26,
        _27 = 27,
        _28 = 28,
        _29 = 29,
        _30 = 30,
    }
    public enum MessageOfCMZTEAirSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMZTEAirSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMZTEAirSettingZteAir
    {
        _0 = 0,
        _1 = 1,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
        _14 = 14,
        _15 = 15,
        _16 = 16,
        _17 = 17,
        _18 = 18,
        _19 = 19,
        _20 = 20,
        _21 = 21,
        _22 = 22,
        _23 = 23,
        _24 = 24,
        _25 = 25,
        _26 = 26,
        _27 = 27,
        _28 = 28,
        _29 = 29,
        _30 = 30,
        _31 = 31,
        _32 = 32,
        _112 = 112,
        _113 = 113,
        _114 = 114,
        _115 = 115,
        _116 = 116,
        _117 = 117,
        _118 = 118,
        _119 = 119,
        _120 = 120,
        _121 = 121,
        _122 = 122,
        _123 = 123,
        _124 = 124,
        _125 = 125,
        _126 = 126,
        _127 = 127,
        _128 = 128,
        _129 = 129,
        _130 = 130,
        _131 = 131,
        _132 = 132,
        _133 = 133,
        _134 = 134,
        _135 = 135,
        _136 = 136,
        _137 = 137,
        _138 = 138,
        _139 = 139,
        _140 = 140,
        _141 = 141,
        _142 = 142,
        _143 = 143,
        _144 = 144,
        _145 = 145,
        _146 = 146,
        _147 = 147,
        _148 = 148,
        _149 = 149,
        _150 = 150,
        _151 = 151,
        _152 = 152,
        _153 = 153,
        _154 = 154,
        _155 = 155,
    }
    public enum MessageOfCMZTEGroundHeatingSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMZTEGroundHeatingSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMZTEGroundHeatingSettingZteGroundHeating
    {
        _0 = 0,
        _1 = 1,
        _16 = 16,
        _17 = 17,
        _18 = 18,
        _19 = 19,
        _20 = 20,
        _21 = 21,
        _22 = 22,
        _23 = 23,
        _24 = 24,
        _25 = 25,
        _26 = 26,
        _27 = 27,
        _28 = 28,
        _29 = 29,
        _30 = 30,
        _31 = 31,
        _32 = 32,
    }
    public enum MessageOfCMHubPowerApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMHubPowerMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMHubPowerDeviceType
    {
        _6 = 6,
        _7 = 7,
        _8 = 8,
        _9 = 9,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
        _14 = 14,
        _15 = 15,
        _16 = 16,
        _17 = 17,
        _18 = 18,
        _19 = 19,
        _20 = 20,
        _21 = 21,
        _22 = 22,
        _23 = 23,
        _25 = 25,
        _26 = 26,
        _27 = 27,
        _28 = 28,
        _29 = 29,
        _30 = 30,
        _31 = 31,
        _32 = 32,
        _35 = 35,
        _36 = 36,
        _37 = 37,
        _38 = 38,
        _39 = 39,
        _41 = 41,
        _42 = 42,
        _43 = 43,
        _44 = 44,
        _45 = 45,
        _46 = 46,
        _47 = 47,
        _48 = 48,
        _49 = 49,
        _52 = 52,
        _53 = 53,
        _54 = 54,
        _55 = 55,
        _56 = 56,
        _58 = 58,
    }
    public enum MessageOfInt32Api
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfInt32Mode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum MessageOfCMHubUpPowerApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMHubUpPowerMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMHubUpPowerType
    {
        _6 = 6,
        _7 = 7,
        _8 = 8,
        _9 = 9,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
        _14 = 14,
        _15 = 15,
        _16 = 16,
        _17 = 17,
        _18 = 18,
        _19 = 19,
        _20 = 20,
        _21 = 21,
        _22 = 22,
        _23 = 23,
        _25 = 25,
        _26 = 26,
        _27 = 27,
        _28 = 28,
        _29 = 29,
        _30 = 30,
        _31 = 31,
        _32 = 32,
        _35 = 35,
        _36 = 36,
        _37 = 37,
        _38 = 38,
        _39 = 39,
        _41 = 41,
        _42 = 42,
        _43 = 43,
        _44 = 44,
        _45 = 45,
        _46 = 46,
        _47 = 47,
        _48 = 48,
        _49 = 49,
        _52 = 52,
        _53 = 53,
        _54 = 54,
        _55 = 55,
        _56 = 56,
        _58 = 58,
    }
    public enum MessageOfCMHubUpWhApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMHubUpWhMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMHubUpWhType
    {
        _6 = 6,
        _7 = 7,
        _8 = 8,
        _9 = 9,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
        _14 = 14,
        _15 = 15,
        _16 = 16,
        _17 = 17,
        _18 = 18,
        _19 = 19,
        _20 = 20,
        _21 = 21,
        _22 = 22,
        _23 = 23,
        _25 = 25,
        _26 = 26,
        _27 = 27,
        _28 = 28,
        _29 = 29,
        _30 = 30,
        _31 = 31,
        _32 = 32,
        _35 = 35,
        _36 = 36,
        _37 = 37,
        _38 = 38,
        _39 = 39,
        _41 = 41,
        _42 = 42,
        _43 = 43,
        _44 = 44,
        _45 = 45,
        _46 = 46,
        _47 = 47,
        _48 = 48,
        _49 = 49,
        _52 = 52,
        _53 = 53,
        _54 = 54,
        _55 = 55,
        _56 = 56,
        _58 = 58,
    }
    public enum MessageOfCMMideaCentralAirSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMMideaCentralAirSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMMideaCentralAirSettingCmSwitch
    {
        _0 = 0,
        _1 = 1,
    }
    public enum CMMideaCentralAirSettingMode
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
    }
    public enum CMMideaCentralAirSettingSpeed
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
    }
    public enum MessageOfCMBackgroundMusicSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMBackgroundMusicSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMBackgroundMusicSettingMusicCtrl
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
    }
    public enum CMBackgroundMusicSettingSwitchMode
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
    }
    public enum MessageOfCMHitachiCentralAirSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMHitachiCentralAirSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMHitachiCentralAirSettingCmSwitch
    {
        _0 = 0,
        _1 = 1,
    }
    public enum CMHitachiCentralAirSettingMode
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
    }
    public enum CMHitachiCentralAirSettingSpeed
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
    }
    public enum MessageOfCMYiupFreshAirSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMYiupFreshAirSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMYiupFreshAirSettingCmSwitch
    {
        _0 = 0,
        _1 = 1,
    }
    public enum CMYiupFreshAirSettingWindSpeed
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
    }
    public enum MessageOfCMHesGroundHeatingControlApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMHesGroundHeatingControlMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMHesGroundHeatingControlCmSwitch
    {
        _0 = 0,
        _1 = 1,
    }
    public enum MessageOfCMHesGroundHeatingSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMHesGroundHeatingSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMHesGroundHeatingSettingCmSwitch
    {
        _0 = 0,
        _1 = 1,
    }
    public enum MessageOfCMHesSmartLockControlApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMHesSmartLockControlMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum MessageOfCMEnvironDetectorApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfCMEnvironDetectorMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum CMEnvironDetectorType
    {
        _6 = 6,
        _7 = 7,
        _8 = 8,
        _9 = 9,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
        _14 = 14,
        _15 = 15,
        _16 = 16,
        _17 = 17,
        _18 = 18,
        _19 = 19,
        _20 = 20,
        _21 = 21,
        _22 = 22,
        _23 = 23,
        _25 = 25,
        _26 = 26,
        _27 = 27,
        _28 = 28,
        _29 = 29,
        _30 = 30,
        _31 = 31,
        _32 = 32,
        _35 = 35,
        _36 = 36,
        _37 = 37,
        _38 = 38,
        _39 = 39,
        _41 = 41,
        _42 = 42,
        _43 = 43,
        _44 = 44,
        _45 = 45,
        _46 = 46,
        _47 = 47,
        _48 = 48,
        _49 = 49,
        _52 = 52,
        _53 = 53,
        _54 = 54,
        _55 = 55,
        _56 = 56,
        _58 = 58,
    }
    public enum MessageOfTimeoutDataSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfTimeoutDataSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum MessageOfStringApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfStringMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum MessageOfUploadLogSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfUploadLogSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum UploadLogSettingLogLevel
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum MessageOfSysCtrlSettingApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfSysCtrlSettingMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum ApiResultOfCMDeivceDataCode
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
    }
    public enum ApiResultOfCMSceneInfoOfCode
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
    }
    public enum ApiResultOfCMSmartLockBodyOfCode
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
    }
    public enum CMTempPassWordSetting2EffectMode
    {
        _0 = 0,
        _1 = 1,
    }
    public enum CMTempPassWordSetting2ActionType
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
    }
    public enum CMTempPassWordSetting2State
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
    }
    public enum ApiResultOfCMTempPassWordBodyOfCode
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
    }
    public enum ApiResultOfCMInfAirKeyStateOfCode
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
    }
    public enum ApiResultOfCMInfTvKeyStateOfCode
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
    }
    public enum ApiResultOfCMInfTopboxKeyStateOfCode
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
    }
    public enum ApiResultOfCMInfDvdKeyStateOfCode
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
    }
    public enum ApiResultOfCMInfTeleOneKeyStateOfCode
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
    }
    public enum ApiResultOfCMInfTeleTwoKeyStateOfCode
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
    }
    public enum ApiResultOfInt16Code
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
    }
    public enum MessageOfEmptyInfoApi
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _100 = 100,
        _101 = 101,
        _1000 = 1000,
        _2000 = 2000,
        _2001 = 2001,
        _2002 = 2002,
        _2003 = 2003,
        _2007 = 2007,
        _2008 = 2008,
        _2009 = 2009,
        _3002 = 3002,
        _3007 = 3007,
        _3008 = 3008,
        _3009 = 3009,
        _3010 = 3010,
        _3011 = 3011,
        _3012 = 3012,
        _3013 = 3013,
        _3014 = 3014,
        _3015 = 3015,
        _3016 = 3016,
        _3017 = 3017,
        _3018 = 3018,
        _3019 = 3019,
        _3020 = 3020,
        _3022 = 3022,
        _3023 = 3023,
        _3024 = 3024,
        _3025 = 3025,
        _3026 = 3026,
        _3027 = 3027,
        _3028 = 3028,
        _3029 = 3029,
        _3031 = 3031,
        _3100 = 3100,
        _3101 = 3101,
        _3102 = 3102,
        _3103 = 3103,
        _3104 = 3104,
        _3105 = 3105,
        _3106 = 3106,
        _3107 = 3107,
        _3108 = 3108,
        _3109 = 3109,
        _3110 = 3110,
        _3111 = 3111,
        _3112 = 3112,
        _3113 = 3113,
        _3114 = 3114,
        _3115 = 3115,
        _3116 = 3116,
        _3117 = 3117,
        _3118 = 3118,
        _3119 = 3119,
        _3120 = 3120,
        _3121 = 3121,
        _3122 = 3122,
        _3123 = 3123,
        _3124 = 3124,
        _3125 = 3125,
        _3126 = 3126,
        _3127 = 3127,
        _3128 = 3128,
        _3129 = 3129,
        _3130 = 3130,
        _3131 = 3131,
        _3132 = 3132,
        _3133 = 3133,
        _3134 = 3134,
        _3135 = 3135,
        _3136 = 3136,
        _3137 = 3137,
        _3138 = 3138,
        _3139 = 3139,
        _3140 = 3140,
        _4000 = 4000,
        _4001 = 4001,
    }
    public enum MessageOfEmptyInfoMode
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
    }
    public enum ApiResultOfStringOfCode
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
    }
    public enum ApiResultOfIntercomAccount_DtoCode
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
    }
    public enum ApiResultOfLauncherCode
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
    }
    public enum ApiResultOfSignedApkOfCode
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
    }
    public enum ApiResultOfMqttServiceCode
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
    }
    public enum CallhangupNotifyCallType
    {
        _0 = 0,
        _1 = 1,
    }
    public enum CallhangupNotifyCalledType
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
    }
    public enum CallhangupNotifyReason
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _5 = 5,
        _6 = 6,
        _7 = 7,
        _8 = 8,
        _9 = 9,
        _255 = 255,
    }
    public enum CMAlarmNotifyAlarmType
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _5 = 5,
        _6 = 6,
        _7 = 7,
        _8 = 8,
        _9 = 9,
        _10 = 10,
    }
    public enum CmOpenRecordNotifyOpenType
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
    }
    public enum ApiResultOfAppInfoOfCode
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
    }
    public enum ApiResultOfAppInfoGroupOfCode
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
    }
    public enum ApiResultOfPushAccount_DtoCode
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
    }
}
