using Google.Protobuf.Protocol;
using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
	public class ObjectManager : Singleton<ObjectManager>
	{
		object _lock = new object();
		Dictionary<int, BaseObject> _gameobjects = new Dictionary<int, BaseObject>();

        // [OBJ_TYPE(4)][TEMPLATE_ID(8)][ID(20)]
        int _counter = 0;

		public T Spawn<T>(int templateId = 0) where T : BaseObject, new()
		{
			T obj = new T();

			lock (_lock)
			{
				obj.ObjectId = GenerateId(obj.ObjectType, templateId);
				// TODO : _gameobjects Add
			}

			return obj;
		}

		int GenerateId(EGameObjectType type, int templateId)
		{
			lock (_lock)
			{
				return ((int)type << 28) | (templateId << 20) | (_counter++);
			}
		}

		public static EGameObjectType GetObjectTypeFromId(int id)
		{
			int type = (id >> 28) & 0x0F;
			return (EGameObjectType)type;
		}

		public static int GetTemplateIdFromId(int id)
		{
			int templateId = (id >> 20) & 0xFF;
			return templateId;
		}

		public bool Remove(int objectId)
		{
			EGameObjectType objectType = GetObjectTypeFromId(objectId);

			lock (_lock)
			{
				//if (objectType == EGameObjectType.Tetromino)
				//	return _tetrominos.Remove(objectId);
			}

			return false;
		}

		public Tetromino Find(int objectId)
		{
			EGameObjectType objectType = GetObjectTypeFromId(objectId);

			lock (_lock)
			{
				if (objectType == EGameObjectType.Tetromino)
				{
					//if (_tetrominos.TryGetValue(objectId, out Tetromino tetromino))
					//	return tetromino;
				}
			}

			return null;
		}

		public T Find<T>(int objectId) where T : BaseObject, new()
		{
			if (_gameobjects.TryGetValue(objectId, out BaseObject bo))
			{
				return bo as T;
			}

			return null;
		}

	}
}
