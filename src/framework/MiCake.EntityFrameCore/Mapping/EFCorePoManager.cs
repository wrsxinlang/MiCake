﻿using MiCake.DDD.Domain;
using MiCake.DDD.Extensions.Store;
using MiCake.DDD.Extensions.Store.Mapping;
using System;
using System.Collections.Generic;

namespace MiCake.EntityFrameworkCore.Mapping
{
    public class EFCorePoManager<TDomainEntity, TPersistentObject> : IDisposable
        where TDomainEntity : IEntity
        where TPersistentObject : IPersistentObject
    {
        private readonly IPersistentObjectReferenceMap _referenceMap = new EFCorePoReferenceMap();
        private readonly IPersistentObjectMapper _mapper;

        public EFCorePoManager(IPersistentObjectMapper persistentObjectMapper)
        {
            _mapper = persistentObjectMapper;
        }

        public void Dispose()
        {
            _referenceMap.Release();
        }

        public TDomainEntity MapToDO(TPersistentObject persistentObject)
        {
            TDomainEntity result;

            if (_referenceMap.TryGetDomainEntity(persistentObject, out var relatedDomainEntity))
            {
                //If can find the associated result, change it based on it.
                result = _mapper.ToDomainEntity(persistentObject, (TDomainEntity)relatedDomainEntity);
            }
            else
            {
                var domainEntity = _mapper.ToDomainEntity<TDomainEntity, TPersistentObject>(persistentObject);
                _referenceMap.Add(domainEntity, persistentObject);

                result = domainEntity;
            }

            return result;
        }

        public IEnumerable<TDomainEntity> MapToDO(IEnumerable<TPersistentObject> persistentObject)
        {
            IEnumerable<TDomainEntity> result;

            if (_referenceMap.TryGetDomainEntity(persistentObject, out var relatedDomainEntity))
            {
                //If can find the associated result, change it based on it.
                result = _mapper.Map(persistentObject, (IEnumerable<TDomainEntity>)relatedDomainEntity);
            }
            else
            {
                var domainEntity = _mapper.Map<IEnumerable<TPersistentObject>, IEnumerable<TDomainEntity>>(persistentObject);
                _referenceMap.Add(domainEntity, persistentObject);

                result = domainEntity;
            }

            return result;
        }

        public TPersistentObject MapToPO(TDomainEntity domainEntity)
        {
            TPersistentObject result;

            if (_referenceMap.TryGetPersistentObj(domainEntity, out var relatedPersistentObj))
            {
                //If can find the associated result, change it based on it.
                result = _mapper.ToPersistentObject(domainEntity, (TPersistentObject)relatedPersistentObj);
            }
            else
            {
                var persistentObjInstance = _mapper.ToPersistentObject<TDomainEntity, TPersistentObject>(domainEntity);
                _referenceMap.Add(domainEntity, persistentObjInstance);

                result = persistentObjInstance;
            }

            return result;
        }

        public IEnumerable<TPersistentObject> MapToPO(IEnumerable<TDomainEntity> domainEntity)
        {
            IEnumerable<TPersistentObject> result;

            if (_referenceMap.TryGetPersistentObj(domainEntity, out var relatedPersistentObj))
            {
                //If can find the associated result, change it based on it.
                result = _mapper.Map(domainEntity, (IEnumerable<TPersistentObject>)relatedPersistentObj);
            }
            else
            {
                var persistentObjInstance = _mapper.Map<IEnumerable<TDomainEntity>, IEnumerable<TPersistentObject>>(domainEntity);
                _referenceMap.Add(domainEntity, persistentObjInstance);

                result = persistentObjInstance;
            }

            return result;
        }
    }
}
