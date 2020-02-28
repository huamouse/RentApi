namespace CPTech.Models
{
    /// <summary>
    /// 响应实体类
    /// </summary>
    public class ResultModel
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data">返回数据</param>
        /// <returns></returns>
        public static ResultModel Ok(object data) => new ResultModel { Code = 200, Data = data };

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="msg">错误信息</param>
        /// <returns></returns>
        public static ResultModel Error(int code, string msg) => new ResultModel { Code = code, Message = msg };
    }
}
