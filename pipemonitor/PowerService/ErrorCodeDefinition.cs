using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///ErrorCodeDefinition 的摘要说明
/// </summary>
//namespace BLL
//{
    public class ErrorCodeDefinition
    {
        public const Int32 DB_ERROR = 100;
        public const Int32 USER_NOTEXIST = 200;
        public const Int32 USER_DISABLE = 204;
        public const Int32 USER_PASSWORD_ERROR = 201;
        public const Int32 USER_NEWPASSORDEQUALOLDPASSWORD = 202;
        public const Int32 USER_OLDPASSWORDISNOTCORRECT = 203;
        public const Int32 USER_MODIFYPASSWORDFAIL = 205;
        //206 删除区域失败
        //207 new区域失败
        //208 new管材失败
        //209 new管线失败
       //("210", "更新管线失败");
        //("211", "更新管材失败");
        //"212", "删除管线失败");
        //"213", "删除管材失败"
        //214 new水表失败
        //("215", "更新水表失败");
        //"216", "删除水表失败"
        //"217", "新增管线位置失败"
        //"218", "新增传感器失败"
        //("219", "更新水表失败");
        //("220", "删除传感器失败");
        //("221", "新增传感器配对失败")
        //("222", "新增权限分组失败");



        public const Int32 IDENTIFYING_CODE_NETWORK_ERROR = 220;
        public const Int32 IDENTIFYING_CODE_EXPIRED = 221;
        public const Int32 IDENTIFYING_CODE_ERROR = 222;
        public const Int32 UPDATE_USER_ERROR = 223;
        public const Int32 USER_ACCOUNT_ALREADY_EXISTS = 206;
        public const Int32 USER_QRCODE_FAIL = 320;
        public const Int32 USER_NULL = 420;
        public const Int32 USER_NOT_EXISTS = 421;
        public const Int32 PHOTO_NOT_EXISTS = 422;
        public const Int32 PHOTO_FAIL = 226;
        public const Int32 FEEDBACK_ERROR = 330;
        public const Int32 SESSIONKEY_EMPTY = 3;
        public const Int32 FRIEND_EXISTS = 310;
        public const Int32 FRIEND_ERROR = 311;
        public const Int32 FRIEND_MINE_ERROR = 314;
        public const Int32 FRIEND_NOT_EXISTS = 312;
        public const Int32 FRIEND_DELETE_ERROR = 313;
        public const Int32 DB_NO_DATA = 99;
        public const Int32 SEND_MESSAGE_ERROR = 300;
        public const Int32 INTERACTION_CREATE_ERROR = 501;
        public const Int32 THIRD_ENTITYFAVORITE_EXISTS = 340;
        public const Int32 THIRD_CANCEL_FAVORITE = 341;
        public const Int32 INFORMATION_BEP_EXISTS = 360;
        public const Int32 INFORMATION_BEP_NOTEXISTS = 361;
        public const Int32 SECOND_FAVORITE_EXISTS = 231;
        public const Int32 SECOND_CANCEL_FAVORITE = 233;
        public const Int32 NEWS_BEP_EXISTS = 362;
        public const Int32 NEWS_BEP_NOTEXISTS = 363;
        public const Int32 INTERACTION_BEP_EXISTS = 364;
        public const Int32 INTERACTION_BEP_NOTEXISTS = 365;
        public const Int32 THIRD_BEP_EXISTS = 366;
        public const Int32 THIRD_BEP_NOTEXISTS = 367;
        public const Int32 USER_LABEL_EXISTS = 496;

        public static string getErrorMessageByErrorCode(Int32 ErrorCode)
        {
            string result = string.Empty;
            switch (ErrorCode)
            {
                case DB_ERROR:
                    result = "数据库异常，请和管理员联系！";
                    break;
                case USER_NOTEXIST:
                    result = "用户名不存在";
                    break;
                case USER_PASSWORD_ERROR:
                    result = "用户密码错误";
                    break;
                case USER_NEWPASSORDEQUALOLDPASSWORD:
                    result = "用户新密码和旧密码不能一样";
                    break;
                case USER_MODIFYPASSWORDFAIL:
                    result = "修改密码失败";
                    break;
                    
                case USER_OLDPASSWORDISNOTCORRECT:
                    result = "用户旧密码不正确";
                    break;
                case IDENTIFYING_CODE_NETWORK_ERROR:
                    result = "发送验证码网络错误";
                    break;
                case IDENTIFYING_CODE_EXPIRED:
                    result = "用户验证码已过期，请重新输入";
                    break;
                case IDENTIFYING_CODE_ERROR:
                    result = "用户验证码和发送到手机的验证码不一致，请重新输入";
                    break;
                case USER_ACCOUNT_ALREADY_EXISTS:
                    result = "此账号已经存在";
                    break;
                case USER_QRCODE_FAIL:
                    result = "生成手机用户二维码失败";
                    break;
                case USER_NULL:
                    result = "用户名为空";
                    break;
                case USER_NOT_EXISTS:
                    result = "用户名不存在";
                    break;
                case PHOTO_NOT_EXISTS:
                    result = "没有图片文件";
                    break;
                case PHOTO_FAIL:
                    result = "更新手机用户图像失败";
                    break;
                case UPDATE_USER_ERROR:
                    result = "更新用户资料失败";
                    break;
                case FEEDBACK_ERROR:
                    result = "添加意见反馈失败";
                    break;
                case SESSIONKEY_EMPTY:
                    result = "您的帐号没有登录,请重新登录";
                    break;
                case FRIEND_EXISTS:
                    result = "已经存在该好友";
                    break;
                case FRIEND_ERROR:
                    result = "添加好友失败";
                    break;
                case FRIEND_MINE_ERROR:
                    result = "不能添加自己为好友";
                    break;
                case FRIEND_NOT_EXISTS:
                    result = "好友已经被删除";
                    break;
                case FRIEND_DELETE_ERROR:
                    result = "删除好友失败";
                    break;
                case DB_NO_DATA:
                    result = "无数据";
                    break;
                case SEND_MESSAGE_ERROR:
                    result = "发送聊天消息失败";
                    break;
                case INTERACTION_CREATE_ERROR:
                    result = "互动添加失败";
                    break;
                case THIRD_ENTITYFAVORITE_EXISTS:
                    result = "商品已被收藏";
                    break;
                case THIRD_CANCEL_FAVORITE:
                    result = "商品已经取消收藏了";
                    break;
                case INFORMATION_BEP_EXISTS:
                    result = "改消息你已经点过赞了";
                    break;
                case SECOND_FAVORITE_EXISTS:
                    result = "商铺已经关注了";
                    break;
                case SECOND_CANCEL_FAVORITE:
                    result = "商铺已经取消关注了";
                    break;
                case INFORMATION_BEP_NOTEXISTS:
                    result = "该信息你已经取消点赞了";
                    break;
                case NEWS_BEP_EXISTS:
                    result = "该资讯你已经点过赞了";
                    break;
                case NEWS_BEP_NOTEXISTS:
                    result = "该资讯你已经取消点赞了";
                    break;
                case INTERACTION_BEP_EXISTS:
                    result = "该互动你已经点过赞了";
                    break;
                case INTERACTION_BEP_NOTEXISTS:
                    result = "该互动你已经取消点赞了";
                    break;
                case THIRD_BEP_EXISTS:
                    result = "该商品你已经点过赞了";
                    break;
                case THIRD_BEP_NOTEXISTS:
                    result = "该商品你取消点赞了";
                    break;
                case USER_LABEL_EXISTS:
                    result = "你已设置了该标签";
                    break;
                case USER_DISABLE:
                    result = "用户已禁用";
                    break;
                default:
                    break;
            }

            return result;
        }
    }
//}
