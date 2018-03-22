using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.IRepository
{
    public interface IBaseRepository<T> where T : class, new()
    {
         /// <summary>
         /// 是否存在
         /// </summary>
         /// <param name="whereLamada">过滤条件</param>
         /// <returns></returns>
        bool IsExists(Expression<Func<T, bool>> whereLamada);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="whereLamada">过滤条件</param>
        /// <returns>满足条件的数据</returns>
        IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLamada, bool isAsNoTracking = false);

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="s"></typeparam>
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
        /// <param name="pageSize">每页记录数</param>
        /// <param name="entities">待分页数据</param>
        /// <returns></returns>
        IQueryable<T> LoadPageEntities(int pageIndex, int pageSize, IQueryable<T> entities);

        /// <summary>
        /// 获取满足条件的首行指定列数据
        /// </summary>
        /// <typeparam name="S">数据的类型</typeparam>
        /// <param name="whereLamada">过滤条件</param>
        /// <param name="selector">查询条件</param>
        /// <returns>数据</returns>
        S LoadScalar<S>(Expression<Func<T, bool>> whereLamada, Expression<Func<T, int, S>> selectLamada);

        /// <summary>
        /// 异步获取满足条件的首行指定列数据
        /// </summary>
        /// <typeparam name="S">数据的类型</typeparam>
        /// <param name="whereLamada">过滤条件</param>
        /// <param name="selector">查询条件</param>
        /// <returns>数据</returns>
        Task<S> LoadScalarAsync<S>(Expression<Func<T, bool>> whereLamada, Expression<Func<T, int, S>> selectLamada);

        /// <summary>
        /// 首条数据
        /// </summary>
        /// <param name="whereLamada">过滤表达式</param>
        /// <returns>满足条件的第一条数据</returns>
        T LoadFirst(Expression<Func<T, bool>> whereLamada, bool isAsNoTracking = false);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        /// <returns></returns>
        bool DeleteEntity(T entity);

        bool EditEntity(T entity);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">要添加的实体</param>
        /// <returns>添加的实体</returns>
        T AddEntity(T entity);

        /// <summary>
        /// 添加数组(小批量集合数据使用)
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        //IEnumerable<T> AddRange(IEnumerable<T> entitys);
    }
}
