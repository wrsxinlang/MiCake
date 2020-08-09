using BaseMiCakeApplication.Domain.Events.IdeaEvents;
using BaseMiCakeApplication.WeChat;
using MiCake.DDD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BaseMiCakeApplication.Domain.EventHandlers.IdeaHandlers
{
    public class AddIdeaHandler : IDomainEventHandler<AddIdenEvent>
    {
        private readonly IWorkApi _workApi;

        public AddIdeaHandler(IWorkApi workApi)
        {
            _workApi = workApi;
        }
        public Task HandleAysnc(AddIdenEvent domainEvent, CancellationToken cancellationToken = default)
        {
            //TODO:消息通知审核人审核
            _workApi.SendTextMsg("梦露创意【测试消息】 \n  用户新增了一个创意,点击审核", "wangrenshuang");
            return Task.CompletedTask;
        }
    }

    public class UpdateIdeaHandler : IDomainEventHandler<UpdateIdenEvent>
    {
        private readonly IWorkApi _workApi;

        public UpdateIdeaHandler(IWorkApi workApi)
        {
            _workApi = workApi;
        }
        public Task HandleAysnc(UpdateIdenEvent domainEvent, CancellationToken cancellationToken = default)
        {
            //TODO:消息通知审核人审核
            _workApi.SendTextMsg("梦露创意【测试消息】 \n  用户更新了创意,点击审核", "wangrenshuang");
            return Task.CompletedTask;
        }
    }
}
