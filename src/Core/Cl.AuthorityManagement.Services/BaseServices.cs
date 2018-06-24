using Cl.AuthorityManagement.Data;
using Cl.AuthorityManagement.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Cl.AuthorityManagement.Services
{
    public class BaseServices<T> where T : class, new()
    {

        #region 获取具体的操作类的实例
        protected readonly DbContext CurrentContext = null;     
        protected readonly IBaseRepository<T> CurrentRepository = null;
        public BaseServices(AuthorityManagementContext context, IBaseRepository<T> baseRepository)
        {
            CurrentRepository = baseRepository;
            CurrentContext = context;
        }
        #endregion

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="whereLamada">过滤条件</param>
        /// <returns>是否</returns>
        public bool IsExists(Expression<Func<T, bool>> whereLamada)
        {
            return CurrentRepository.IsExists(whereLamada);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">要添加的实体</param>
        /// <returns></returns>
        public T AddEntity(T entity)
        {
            CurrentRepository.AddEntity(entity);
            CurrentContext.SaveChanges();
            return entity;
        }
        
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        /// <returns></returns>
        public bool DeleteEntity(T entity)
        {
            CurrentRepository.DeleteEntity(entity);
            return CurrentContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity">要修改的实体</param>
        /// <returns>是否成功</returns>
        public bool EditEntity(T entity)
        {
            CurrentRepository.EditEntity(entity);
            return CurrentContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 获取满足条件的首行指定列数据
        /// </summary>
        /// <typeparam name="S">数据的类型</typeparam>
        /// <param name="whereLamada">过滤条件</param>
        /// <param name="selector">查询条件</param>
        /// <returns></returns>
        public S LoadScalar<S>(Expression<Func<T, bool>> whereLamada, Expression<Func<T, int, S>> selectLamada)
        {
            return CurrentRepository.LoadScalar<S>(whereLamada, selectLamada);
        }

        public T LoadFirst(Expression<Func<T, bool>> whereLamada)
        {
            return CurrentRepository.LoadFirst(whereLamada);
        }

        public T LoadFirst(Expression<Func<T, bool>> whereLamada, bool isAsNoTracking = false)
        {
            return CurrentRepository.LoadFirst(whereLamada, isAsNoTracking);

        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="whereLamada">过滤条件</param>
        /// <returns></returns>
        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLamada, bool isAsNoTracking = false)
        {
            return CurrentRepository.LoadEntities(whereLamada, isAsNoTracking);
        }

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
        public IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> whereLamada, Expression<Func<T, s>> orderbyLamada, bool isASC = true, bool isAsNoTracking = false)
        {
            return CurrentRepository.LoadPageEntities(pageIndex, pageSize, out totalCount, whereLamada, orderbyLamada, isASC, isAsNoTracking);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="orderbyLamada">排序条件</param>
        /// <param name="isASC">排序方式</param>
        /// <returns></returns>
        public IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, IQueryable<T> entities, Expression<Func<T, s>> orderbyLamada, bool isASC = true)
        {
            return CurrentRepository.LoadPageEntities<s>(pageIndex, pageSize, entities, orderbyLamada, isASC);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">总页数</param>
        /// <param name="entities">带分页的数据</param>
        /// <returns></returns>
        public IQueryable<T> LoadPageEntities(int pageIndex, int pageSize, IQueryable<T> entities)
        {
            return CurrentRepository.LoadPageEntities(pageIndex, pageSize, entities);
        }
    }
}
