using StackExchange.Redis;
using System.Threading.Tasks;

namespace GrpcDemo.DomainService.Core.Lua
{
    public class LuaScriptHelper
    {
        private readonly ConnectionMultiplexer _conn;

        private readonly string _connString;

        public LuaScriptHelper(ConnectionMultiplexer conn, string connString)
        {
            _conn = conn;
            _connString = connString;
        }

        public async Task<LoadedLuaScript> Load(string script)
        {
            var result = await LuaScript.Prepare(script).LoadAsync(_conn.GetServer(_connString));

            return result;
        }

        public async Task<RedisResult> Evaluate(string script, RedisKey[] keys = null, RedisValue[] values = null)
        {
            var loadedScript = await Load(script);

            var result = await _conn.GetDatabase().ScriptEvaluateAsync(loadedScript.Hash, keys, values);

            return result;
        }
    }
}