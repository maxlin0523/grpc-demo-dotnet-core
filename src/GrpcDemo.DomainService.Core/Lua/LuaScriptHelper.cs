using StackExchange.Redis;
using System.Threading.Tasks;

namespace GrpcDemo.DomainService.Core.Lua
{
    public class LuaScriptHelper
    {
        private readonly ConnectionMultiplexer _conn;

        private readonly string _connString;

        private readonly int _db;

        public LuaScriptHelper(ConnectionMultiplexer conn, string connString, int database = -1)
        {
            _conn = conn;
            _connString = connString;
            _db = database;
        }

        public async Task<LoadedLuaScript> Load(string script)
        {
            var loaded = await LuaScript.Prepare(script).LoadAsync(_conn.GetServer(_connString));

            return loaded;
        }

        public async Task<RedisResult> Execute(string script, RedisKey[] keys = null, RedisValue[] values = null)
        {
            var loadedScript = await Load(script);

            var result = await _conn.GetDatabase(_db).ScriptEvaluateAsync(loadedScript.Hash, keys, values);

            return result;
        }
    }
}