﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.SysHelper
{
    public class EnumData
    {
        public enum EnumMenuName { 首页, 禁止匿名访问, 匿名访问 };

        public enum EnumOperationAccess { 浏览 = 1, 新增 = 2, 编辑 = 3, 删除 = 4, 导入 = 5, 导出 = 6 };

        public enum EnumValidateAccess { IsView = 1, IsAdd = 2, IsEdit = 3, IsDelete = 4, IsImport = 5, IsExport = 6 };

        public enum ResCode { 成功 = 1000, 失败 = 1001 };

        public enum EnumIsOk { 否, 是 };

        public enum EnumIsDisable { 启用, 禁用 };

        public enum EnumStatus { 正常 };

        public enum Platform : byte { PC, Android, IOS }

        public enum EnumOrderPrefix { 其它, 采购, 预收货, 收货, 上架, 发货, 拣货,配送 }

        public enum EnumStep { 预收货 = 1, 收货 = 2, 上架 = 3, 发货 = 4, 拣货 = 5, 盘点 = 6 };

        public enum EnumAppId : byte { Ccecc = 1, Cssc = 2 };

        public enum EnumRunQueue { BaiduMapRestApi }

    }
}
