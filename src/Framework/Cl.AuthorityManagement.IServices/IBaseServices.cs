using Cl.AuthorityManagement.IRepository;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Cl.AuthorityManagement.IServices
{
    public interface IBaseServices<T> where T : class, new()
    {
        IDBSession CurrentDBSession { get; }
        //IBaseRepository<T> CurrentDal { get; set; }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="whereLamada">过滤条件</param>
        /// <returns>是否</returns>
        bool IsExists(Expression<Func<T, bool>> whereLamada);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">要添加的实体</param>
        /// <returns></returns>
        T AddEntity(T entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        /// <returns>是否成功</returns>
        bool DeleteEntity(T entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">要修改的实体</param>
        /// <returns>是否成功</returns>
        bool EditEntity(T entity);

        /// <summary>
        /// 获取满足条件的首行指定列数据
        /// </summary>
        /// <typeparam name="S">数据的类型</typeparam>
        /// <param name="whereLamada">过滤条件</param>
        /// <param name="selector">查询条件</param>
        /// <returns>数据</returns>
        S LoadScalar<S>(Expression<Func<T, bool>> whereLamada, Expression<Func<T, int, S>> selectLamada);

        T LoadFirst(Expression<Func<T, bool>> whereLamada);
        /// <summary>
        /// 首条数据
        /// </summary>
        /// <param name="whereLamada">过滤表达式</param>
        /// <returns>满足条件的第一条数据</returns>
        T LoadFirst(Expression<Func<T, bool>> whereLamada, bool isAsNoTracking = false);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="whereLamada">过滤条件</param>
        /// <returns>满足条件的数据</returns>
        IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLamada, bool isAsNoTracking = false);

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="s">排序字段类型</typeparam>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="whereLamada">过滤条件</param>
        /// <param name="orderbyLamada">排序条件</param>
        /// <param name="isASC">排序方式</param>
        /// <returns>分页数据</returns>
        IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> whereLamada, Expression<Func<T, s>> orderbyLamada, bool isASC = true, bool isAsNoTracking = false);

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="orderbyLamada">排序条件</param>
        /// <param name="isASC">排序方式</param>
        /// <returns></returns>
        IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, IQueryable<T> entities, Expression<Func<T, s>> orderbyLamada, bool isASC = true);

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">总页数</param>
        /// <param name="entities">带分页的数据</param>
        /// <returns></returns>
        IQueryable<T> LoadPageEntities(int pageIndex, int pageSize, IQueryable<T> entities);
    }
}
