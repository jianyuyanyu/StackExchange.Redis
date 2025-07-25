﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace StackExchange.Redis
{
    /// <summary>
    /// Provides basic information about the features available on a particular version of Redis.
    /// </summary>
    public readonly struct RedisFeatures : IEquatable<RedisFeatures>
    {
#pragma warning disable SA1310 // Field names should not contain underscore
#pragma warning disable SA1311 // Static readonly fields should begin with upper-case letter
        internal static readonly Version v2_0_0 = new Version(2, 0, 0),
                                         v2_1_0 = new Version(2, 1, 0),
                                         v2_1_1 = new Version(2, 1, 1),
                                         v2_1_2 = new Version(2, 1, 2),
                                         v2_1_3 = new Version(2, 1, 3),
                                         v2_1_8 = new Version(2, 1, 8),
                                         v2_2_0 = new Version(2, 2, 0),
                                         v2_4_0 = new Version(2, 4, 0),
                                         v2_5_7 = new Version(2, 5, 7),
                                         v2_5_10 = new Version(2, 5, 10),
                                         v2_5_14 = new Version(2, 5, 14),
                                         v2_6_0 = new Version(2, 6, 0),
                                         v2_6_5 = new Version(2, 6, 5),
                                         v2_6_9 = new Version(2, 6, 9),
                                         v2_6_12 = new Version(2, 6, 12),
                                         v2_8_0 = new Version(2, 8, 0),
                                         v2_8_12 = new Version(2, 8, 12),
                                         v2_8_18 = new Version(2, 8, 18),
                                         v2_9_5 = new Version(2, 9, 5),
                                         v3_0_0 = new Version(3, 0, 0),
                                         v3_2_0 = new Version(3, 2, 0),
                                         v3_2_1 = new Version(3, 2, 1),
                                         v4_0_0 = new Version(4, 0, 0),
                                         v4_9_1 = new Version(4, 9, 1), // 5.0 RC1 is version 4.9.1; // 5.0 RC1 is version 4.9.1
                                         v5_0_0 = new Version(5, 0, 0),
                                         v6_0_0 = new Version(6, 0, 0),
                                         v6_0_6 = new Version(6, 0, 6),
                                         v6_2_0 = new Version(6, 2, 0),
                                         v7_0_0_rc1 = new Version(6, 9, 240), // 7.0 RC1 is version 6.9.240
                                         v7_2_0_rc1 = new Version(7, 1, 240), // 7.2 RC1 is version 7.1.240
                                         v7_4_0_rc1 = new Version(7, 3, 240), // 7.4 RC1 is version 7.3.240
                                         v7_4_0_rc2 = new Version(7, 3, 241), // 7.4 RC2 is version 7.3.241
                                         v8_0_0_M04 = new Version(7, 9, 227), // 8.0 M04 is version 7.9.227
                                         v8_2_0_rc1 = new Version(8, 1, 240); // 8.2 RC1 is version 8.1.240

#pragma warning restore SA1310 // Field names should not contain underscore
#pragma warning restore SA1311 // Static readonly fields should begin with upper-case letter

        private readonly Version version;

        /// <summary>
        /// Create a new RedisFeatures instance for the given version.
        /// </summary>
        /// <param name="version">The version of redis to base the feature set on.</param>
        public RedisFeatures(Version version)
        {
            this.version = version ?? throw new ArgumentNullException(nameof(version));
        }

#pragma warning disable SA1629 // Documentation should end with a period

        /// <summary>
        /// Are <see href="https://redis.io/commands/bitop/">BITOP</see> and <see href="https://redis.io/commands/bitcount/">BITCOUNT</see> available?
        /// </summary>
        public bool BitwiseOperations => Version.IsAtLeast(v2_6_0);

        /// <summary>
        /// Is <see href="https://redis.io/commands/client-setname/">CLIENT SETNAME</see> available?
        /// </summary>
        public bool ClientName => Version.IsAtLeast(v2_6_9);

        /// <summary>
        /// Is <see href="https://redis.io/commands/client-id/">CLIENT ID</see> available?
        /// </summary>
        public bool ClientId => Version.IsAtLeast(v5_0_0);

        /// <summary>
        /// Does <see href="https://redis.io/commands/exec/">EXEC</see> support <c>EXECABORT</c> if there are errors?
        /// </summary>
        public bool ExecAbort => Version.IsAtLeast(v2_6_5) && !Version.IsEqual(v2_9_5);

        /// <summary>
        /// Can <see href="https://redis.io/commands/expire/">EXPIRE</see> be used to set expiration on a key that is already volatile (i.e. has an expiration)?
        /// </summary>
        public bool ExpireOverwrite => Version.IsAtLeast(v2_1_3);

        /// <summary>
        /// Is <see href="https://redis.io/commands/getdel/">GETDEL</see> available?
        /// </summary>
        public bool GetDelete => Version.IsAtLeast(v6_2_0);

        /// <summary>
        /// Is <see href="https://redis.io/commands/hstrlen/">HSTRLEN</see> available?
        /// </summary>
        public bool HashStringLength => Version.IsAtLeast(v3_2_0);

        /// <summary>
        /// Does <see href="https://redis.io/commands/hdel/">HDEL</see> support variadic usage?
        /// </summary>
        public bool HashVaradicDelete => Version.IsAtLeast(v2_4_0);

        /// <summary>
        /// Are <see href="https://redis.io/commands/incrbyfloat/">INCRBYFLOAT</see> and <see href="https://redis.io/commands/hincrbyfloat/">HINCRBYFLOAT</see> available?
        /// </summary>
        public bool IncrementFloat => Version.IsAtLeast(v2_6_0);

        /// <summary>
        /// Does <see href="https://redis.io/commands/info/">INFO</see> support sections?
        /// </summary>
        public bool InfoSections => Version.IsAtLeast(v2_8_0);

        /// <summary>
        /// Is <see href="https://redis.io/commands/linsert/">LINSERT</see> available?
        /// </summary>
        public bool ListInsert => Version.IsAtLeast(v2_1_1);

        /// <summary>
        /// Is <see href="https://redis.io/commands/memory/">MEMORY</see> available?
        /// </summary>
        public bool Memory => Version.IsAtLeast(v4_0_0);

        /// <summary>
        /// Are <see href="https://redis.io/commands/pexpire/">PEXPIRE</see> and <see href="https://redis.io/commands/pttl/">PTTL</see> available?
        /// </summary>
        public bool MillisecondExpiry => Version.IsAtLeast(v2_6_0);

        /// <summary>
        /// Is <see href="https://redis.io/commands/module/">MODULE</see> available?
        /// </summary>
        public bool Module => Version.IsAtLeast(v4_0_0);

        /// <summary>
        /// Does <see href="https://redis.io/commands/srandmember/">SRANDMEMBER</see> support the "count" option?
        /// </summary>
        public bool MultipleRandom => Version.IsAtLeast(v2_5_14);

        /// <summary>
        /// Is <see href="https://redis.io/commands/persist/">PERSIST</see> available?
        /// </summary>
        public bool Persist => Version.IsAtLeast(v2_1_2);

        /// <summary>
        /// Are <see href="https://redis.io/commands/lpushx/">LPUSHX</see> and <see href="https://redis.io/commands/rpushx/">RPUSHX</see> available?
        /// </summary>
        public bool PushIfNotExists => Version.IsAtLeast(v2_1_1);

        /// <summary>
        /// Does this support <see href="https://redis.io/commands/sort_ro">SORT_RO</see>?
        /// </summary>
        internal bool ReadOnlySort => Version.IsAtLeast(v7_0_0_rc1);

        /// <summary>
        /// Is <see href="https://redis.io/commands/scan/">SCAN</see> (cursor-based scanning) available?
        /// </summary>
        public bool Scan => Version.IsAtLeast(v2_8_0);

        /// <summary>
        /// Are <see href="https://redis.io/commands/eval/">EVAL</see>, <see href="https://redis.io/commands/evalsha/">EVALSHA</see>, and other script commands available?
        /// </summary>
        public bool Scripting => Version.IsAtLeast(v2_6_0);

        /// <summary>
        /// Does <see href="https://redis.io/commands/set/">SET</see> support the <c>GET</c> option?
        /// </summary>
        public bool SetAndGet => Version.IsAtLeast(v6_2_0);

        /// <summary>
        /// Does <see href="https://redis.io/commands/set/">SET</see> support the <c>EX</c>, <c>PX</c>, <c>NX</c>, and <c>XX</c> options?
        /// </summary>
        public bool SetConditional => Version.IsAtLeast(v2_6_12);

        /// <summary>
        /// Does <see href="https://redis.io/commands/set/">SET</see> have the <c>KEEPTTL</c> option?
        /// </summary>
        public bool SetKeepTtl => Version.IsAtLeast(v6_0_0);

        /// <summary>
        /// Does <see href="https://redis.io/commands/set/">SET</see> allow the <c>NX</c> and <c>GET</c> options to be used together?
        /// </summary>
        public bool SetNotExistsAndGet => Version.IsAtLeast(v7_0_0_rc1);

        /// <summary>
        /// Does <see href="https://redis.io/commands/sadd/">SADD</see> support variadic usage?
        /// </summary>
        public bool SetVaradicAddRemove => Version.IsAtLeast(v2_4_0);

        /// <summary>
        /// Are <see href="https://redis.io/commands/ssubscribe/">SSUBSCRIBE</see> and <see href="https://redis.io/commands/spublish/">SPUBLISH</see> available?
        /// </summary>
        public bool ShardedPubSub => Version.IsAtLeast(v7_0_0_rc1);

        /// <summary>
        /// Are <see href="https://redis.io/commands/zpopmin/">ZPOPMIN</see> and <see href="https://redis.io/commands/zpopmax/">ZPOPMAX</see> available?
        /// </summary>
        public bool SortedSetPop => Version.IsAtLeast(v5_0_0);

        /// <summary>
        /// Is <see href="https://redis.io/commands/zrangestore/">ZRANGESTORE</see> available?
        /// </summary>
        public bool SortedSetRangeStore => Version.IsAtLeast(v6_2_0);

        /// <summary>
        /// Are <see href="https://redis.io/topics/streams-intro">Redis Streams</see> available?
        /// </summary>
        public bool Streams => Version.IsAtLeast(v4_9_1);

        /// <summary>
        /// Is <see href="https://redis.io/commands/strlen/">STRLEN</see> available?
        /// </summary>
        public bool StringLength => Version.IsAtLeast(v2_1_2);

        /// <summary>
        /// Is <see href="https://redis.io/commands/setrange/">SETRANGE</see> available?
        /// </summary>
        public bool StringSetRange => Version.IsAtLeast(v2_1_8);

        /// <summary>
        /// Is <see href="https://redis.io/commands/swapdb/">SWAPDB</see> available?
        /// </summary>
        public bool SwapDB => Version.IsAtLeast(v4_0_0);

        /// <summary>
        /// Is <see href="https://redis.io/commands/time/">TIME</see> available?
        /// </summary>
        public bool Time => Version.IsAtLeast(v2_6_0);

        /// <summary>
        /// Is <see href="https://redis.io/commands/unlink/">UNLINK</see> available?
        /// </summary>
        public bool Unlink => Version.IsAtLeast(v4_0_0);

        /// <summary>
        /// Are Lua changes to the calling database transparent to the calling client?
        /// </summary>
        public bool ScriptingDatabaseSafe => Version.IsAtLeast(v2_8_12);

        /// <inheritdoc cref="HyperLogLogCountReplicaSafe"/>
        [Obsolete("Starting with Redis version 5, Redis has moved to 'replica' terminology. Please use " + nameof(HyperLogLogCountReplicaSafe) + " instead, this will be removed in 3.0.")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool HyperLogLogCountSlaveSafe => HyperLogLogCountReplicaSafe;

        /// <summary>
        /// Is <see href="https://redis.io/commands/pfcount/">PFCOUNT</see> available on replicas?
        /// </summary>
        public bool HyperLogLogCountReplicaSafe => Version.IsAtLeast(v2_8_18);

        /// <summary>
        /// Are <see href="https://redis.io/commands/?group=geo">geospatial commands</see> available?
        /// </summary>
        public bool Geo => Version.IsAtLeast(v3_2_0);

        /// <summary>
        /// Can <see href="https://redis.io/commands/ping/">PING</see> be used on a subscription connection?
        /// </summary>
        internal bool PingOnSubscriber => Version.IsAtLeast(v3_0_0);

        /// <summary>
        /// Does <see href="https://redis.io/commands/spop/">SPOP</see> support popping multiple items?
        /// </summary>
        public bool SetPopMultiple => Version.IsAtLeast(v3_2_0);

        /// <summary>
        /// Is <see href="https://redis.io/commands/touch/">TOUCH</see> available?
        /// </summary>
        public bool KeyTouch => Version.IsAtLeast(v3_2_1);

        /// <summary>
        /// Does the server prefer 'replica' terminology - '<see href="https://redis.io/commands/replicaof/">REPLICAOF</see>', etc?
        /// </summary>
        public bool ReplicaCommands => Version.IsAtLeast(v5_0_0);

        /// <summary>
        /// Do list-push commands support multiple arguments?
        /// </summary>
        public bool PushMultiple => Version.IsAtLeast(v4_0_0);

        /// <summary>
        /// Is the RESP3 protocol available?
        /// </summary>
        public bool Resp3 => Version.IsAtLeast(v6_0_0);

#pragma warning restore 1629 // Documentation text should end with a period.

        /// <summary>
        /// The Redis version of the server.
        /// </summary>
        public Version Version => version ?? v2_0_0;

        /// <summary>
        /// Create a string representation of the available features.
        /// </summary>
        public override string ToString()
        {
            var v = Version; // the docs lie: Version.ToString(fieldCount) only supports 0-2 fields
            var sb = new StringBuilder().Append("Features in ").Append(v.Major).Append('.').Append(v.Minor);
            if (v.Revision >= 0) sb.Append('.').Append(v.Revision);
            if (v.Build >= 0) sb.Append('.').Append(v.Build);
            sb.AppendLine();
            object boxed = this;
            foreach (var prop in s_props)
            {
                sb.Append(prop.Name).Append(": ").Append(prop.GetValue(boxed)).AppendLine();
            }
            return sb.ToString();
        }

        private static readonly PropertyInfo[] s_props = (
            from prop in typeof(RedisFeatures).GetProperties(BindingFlags.Instance | BindingFlags.Public)
            where prop.PropertyType == typeof(bool)
            let indexers = prop.GetIndexParameters()
            where indexers == null || indexers.Length == 0
            orderby prop.Name
            select prop).ToArray();

        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode() => Version.GetNormalizedHashCode();

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if <paramref name="obj" /> and this instance are the same type and represent the same value, <see langword="false"/> otherwise.
        /// </returns>
        /// <param name="obj">The object to compare with the current instance.</param>
        public override bool Equals(object? obj) => obj is RedisFeatures f && f.Version.IsEqual(Version);

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if <paramref name="other" /> and this instance are the same type and represent the same value, <see langword="false"/> otherwise.
        /// </returns>
        /// <param name="other">The object to compare with the current instance.</param>
        public bool Equals(RedisFeatures other) => other.Version.IsEqual(Version);

        /// <summary>
        /// Checks if 2 <see cref="RedisFeatures"/> are .Equal().
        /// </summary>
        public static bool operator ==(RedisFeatures left, RedisFeatures right) => left.Version.IsEqual(right.Version);

        /// <summary>
        /// Checks if 2 <see cref="RedisFeatures"/> are not .Equal().
        /// </summary>
        public static bool operator !=(RedisFeatures left, RedisFeatures right) => !left.Version.IsEqual(right.Version);
    }
}

internal static class VersionExtensions
{
    // normalize two version parts and smash them together into a long; if either part is -ve,
    // zero is used instead; this gives us consistent ordering following "long" rules
    private static long ComposeMajorMinor(Version version) // always specified
        => (((long)version.Major) << 32) | (long)version.Minor;

    private static long ComposeBuildRevision(Version version) // can be -ve for "not specified"
    {
        int build = version.Build, revision = version.Revision;
        return (((long)(build < 0 ? 0 : build)) << 32) | (long)(revision < 0 ? 0 : revision);
    }

    internal static int GetNormalizedHashCode(this Version value)
        => (ComposeMajorMinor(value) * ComposeBuildRevision(value)).GetHashCode();

    internal static bool IsEqual(this Version x, Version y)
        => ComposeMajorMinor(x) == ComposeMajorMinor(y)
            && ComposeBuildRevision(x) == ComposeBuildRevision(y);

    internal static bool IsAtLeast(this Version x, Version y)
    {
        // think >=, but: without the... "unusual behaviour" in how Version's >= operator
        // compares values with different part lengths, i.e. "6.0" **is not** >= "6.0.0"
        // under the inbuilt operator
        var delta = ComposeMajorMinor(x) - ComposeMajorMinor(y);
        if (delta > 0) return true;
        return delta < 0 ? false : ComposeBuildRevision(x) >= ComposeBuildRevision(y);
    }
}
