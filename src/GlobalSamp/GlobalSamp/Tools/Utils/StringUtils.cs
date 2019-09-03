/* Author: WilliamSXE */

using System;
using System.Text;

namespace BetAppServer.Tools.Utils
{
    public static class StringUtils
    {
        private const float ENCODING_THRESHOLD = 0.1f;

        public static Encoding DetermineEncoding(string src, int taster = 1000)
        {
            if (src.Length >= 4 && src[0] == 0x00 && src[1] == 0x00 && src[2] == 0xFE && src[3] == 0xFF)
            {
                return Encoding.GetEncoding("utf-32BE");
            }

            if (src.Length >= 4 && src[0] == 0xFF && src[1] == 0xFE && src[2] == 0x00 && src[3] == 0x00)
            {
                return Encoding.UTF32;
            }

            if (src.Length >= 2 && src[0] == 0xFE && src[1] == 0xFF)
            {
                return Encoding.BigEndianUnicode;
            }

            if (src.Length >= 2 && src[0] == 0xFF && src[1] == 0xFE)
            {
                return Encoding.Unicode;
            }

            if (src.Length >= 3 && src[0] == 0xEF && src[1] == 0xBB && src[2] == 0xBF)
            {
                return Encoding.UTF8;
            }

            if (src.Length >= 3 && src[0] == 0x2b && src[1] == 0x2f && src[2] == 0x76)
            {
                return Encoding.UTF7;
            }

            if (taster == 0 || taster > src.Length)
            {
                taster = src.Length;
            }

            int i = 0;
            bool utf8 = false;

            int tasterMask = taster - 4;
            while (i < tasterMask)
            {
                if (src[i] <= 0x7F)
                {
                    i += 1;
                    continue;
                }

                if (src[i] >= 0xC2 && src[i] <= 0xDF && src[i + 1] >= 0x80 && src[i + 1] < 0xC0)
                {
                    i += 2;
                    utf8 = true;
                    continue;
                }

                if (src[i] >= 0xE0 && src[i] <= 0xF0 && src[i + 1] >= 0x80 && src[i + 1] < 0xC0 &&
                    src[i + 2] >= 0x80 && src[i + 2] < 0xC0)
                {
                    i += 3;
                    utf8 = true;
                    continue;
                }

                if (src[i] >= 0xF0 && src[i] <= 0xF4 && src[i + 1] >= 0x80 && src[i + 1] < 0xC0 &&
                    src[i + 2] >= 0x80 && src[i + 2] < 0xC0 && src[i + 3] >= 0x80 && src[i + 3] < 0xC0)
                {
                    i += 4;
                    utf8 = true;
                    continue;
                }

                utf8 = false;
                break;
            }

            if (utf8)
            {
                return Encoding.UTF8;
            }

            float count = 0;
            for (i = 0; i < taster; i += 2)
            {
                if (src[i] == 0)
                {
                    count++;
                }
            }

            if (count / taster > ENCODING_THRESHOLD)
            {
                return Encoding.BigEndianUnicode;
            }

            count = 0;
            for (i = 1; i < taster; i += 2)
            {
                if (src[i] == 0)
                {
                    count++;
                }
            }

            if (count / taster > ENCODING_THRESHOLD)
            {
                return Encoding.Unicode;
            }

            tasterMask = taster - 9;
            byte[] cache;
            for (i = 0; i < tasterMask; i++)
            {
                if ((src[i] != 'c' && src[i] != 'C' || (src[i + 1] != 'h' && src[i + 1] != 'H') ||
                     src[i + 2] != 'a' && src[i + 2] != 'A' || (src[i + 3] != 'r' && src[i + 3] != 'R') ||
                     src[i + 4] != 's' && src[i + 4] != 'S' || (src[i + 5] != 'e' && src[i + 5] != 'E') ||
                     src[i + 6] != 't' && src[i + 6] != 'T' || (src[i + 7] != '=')) &&
                    (src[i] != 'e' && src[i] != 'E' || (src[i + 1] != 'n' && src[i + 1] != 'N') ||
                     src[i + 2] != 'c' && src[i + 2] != 'C' || (src[i + 3] != 'o' && src[i + 3] != 'O') ||
                     src[i + 4] != 'd' && src[i + 4] != 'D' || (src[i + 5] != 'i' && src[i + 5] != 'I') ||
                     src[i + 6] != 'n' && src[i + 6] != 'N' || (src[i + 7] != 'g' && src[i + 7] != 'G') ||
                     src[i + 8] != '='))
                {
                    continue;
                }

                if (src[i + 0] == 'c' || src[i + 0] == 'C') i += 8;
                else i += 9;
                if (src[i] == '"' || src[i] == '\'') i++;
                int oldIter = i;
                while (i < taster && (src[i] == '_' || src[i] == '-' || src[i] >= '0' && src[i] <= '9' ||
                                      src[i] >= 'a' && src[i] <= 'z' || src[i] >= 'A' && src[i] <= 'Z'))
                {
                    i++;
                }

                cache = new byte[i - oldIter];
                Array.Copy(src.ToCharArray(), oldIter, cache, 0, i - oldIter);
                try
                {
                    string internalEncoding = Encoding.ASCII.GetString(cache);
                    return Encoding.GetEncoding(internalEncoding);
                }
                catch
                {
                    break;
                }
            }

            return Encoding.Default;
        }

        public static string GetDataSize(double b)
        {
            var sb = new StringBuilder();

            long bytes = (long) b;
            long absBytes = Math.Abs(bytes);

            if (absBytes >= (1024L * 1024L * 1024L * 1024L))
            {
                long tb = bytes / (1024L * 1024L * 1024L * 1024L);
                long gb = (bytes % (1024L * 1024L * 1024L * 1024L)) / (1024 * 1024 * 1024);
                sb.Append(tb);
                sb.Append('.');
                sb.Append((gb < 100) ? "0" : "");
                sb.Append((gb < 10) ? "0" : "");
                sb.Append(gb);
                sb.Append(" TiB");
            }
            else if (absBytes >= (1024 * 1024 * 1024))
            {
                long gb = bytes / (1024 * 1024 * 1024);
                long mb = (bytes % (1024 * 1024 * 1024)) / (1024 * 1024);
                sb.Append(gb);
                sb.Append('.');
                sb.Append((mb < 100) ? "0" : "");
                sb.Append((mb < 10) ? "0" : "");
                sb.Append(mb);
                sb.Append(" GiB");
            }
            else if (absBytes >= (1024 * 1024))
            {
                long mb = bytes / (1024 * 1024);
                long kb = (bytes % (1024 * 1024)) / 1024;
                sb.Append(mb);
                sb.Append('.');
                sb.Append((kb < 100) ? "0" : "");
                sb.Append((kb < 10) ? "0" : "");
                sb.Append(kb);
                sb.Append(" MiB");
            }
            else if (absBytes >= 1024)
            {
                long kb = bytes / 1024;
                bytes = bytes % 1024;
                sb.Append(kb);
                sb.Append('.');
                sb.Append((bytes < 100) ? "0" : "");
                sb.Append((bytes < 10) ? "0" : "");
                sb.Append(bytes);
                sb.Append(" KiB");
            }
            else
            {
                sb.Append(bytes);
                sb.Append(" bytes");
            }

            return sb.ToString();
        }

        public static string GetTimePeriod(double ms)
        {
            var sb = new StringBuilder();

            long nanoseconds = (long) (ms * 1000.0 * 1000.0);
            long absNanoseconds = Math.Abs(nanoseconds);

            if (absNanoseconds >= (60 * 60 * 1000000000L))
            {
                long hours = nanoseconds / (60 * 60 * 1000000000L);
                long minutes = ((nanoseconds % (60 * 60 * 1000000000L)) / 1000000000) / 60;
                long seconds = ((nanoseconds % (60 * 60 * 1000000000L)) / 1000000000) % 60;
                long milliseconds = ((nanoseconds % (60 * 60 * 1000000000L)) % 1000000000) / 1000000;
                sb.Append(hours);
                sb.Append(':');
                sb.Append((minutes < 10) ? "0" : "");
                sb.Append(minutes);
                sb.Append(':');
                sb.Append((seconds < 10) ? "0" : "");
                sb.Append(seconds);
                sb.Append('.');
                sb.Append((milliseconds < 100) ? "0" : "");
                sb.Append((milliseconds < 10) ? "0" : "");
                sb.Append(milliseconds);
                sb.Append(" h");
            }
            else if (absNanoseconds >= (60 * 1000000000L))
            {
                long minutes = nanoseconds / (60 * 1000000000L);
                long seconds = nanoseconds % (60 * 1000000000L) / 1000000000;
                long milliseconds = ((nanoseconds % (60 * 1000000000L)) % 1000000000) / 1000000;
                sb.Append(minutes);
                sb.Append(':');
                sb.Append((seconds < 10) ? "0" : "");
                sb.Append(seconds);
                sb.Append('.');
                sb.Append((milliseconds < 100) ? "0" : "");
                sb.Append((milliseconds < 10) ? "0" : "");
                sb.Append(milliseconds);
                sb.Append(" m");
            }
            else if (absNanoseconds >= 1000000000)
            {
                long seconds = nanoseconds / 1000000000;
                long milliseconds = nanoseconds % 1000000000 / 1000000;
                sb.Append(seconds);
                sb.Append('.');
                sb.Append((milliseconds < 100) ? "0" : "");
                sb.Append((milliseconds < 10) ? "0" : "");
                sb.Append(milliseconds);
                sb.Append(" s");
            }
            else if (absNanoseconds >= 1000000)
            {
                long milliseconds = nanoseconds / 1000000;
                long microseconds = (nanoseconds % 1000000) / 1000;
                sb.Append(milliseconds);
                sb.Append('.');
                sb.Append(microseconds < 100 ? "0" : "");
                sb.Append(microseconds < 10 ? "0" : "");
                sb.Append(microseconds);
                sb.Append(" ms");
            }
            else if (absNanoseconds >= 1000)
            {
                long microseconds = nanoseconds / 1000;
                nanoseconds = nanoseconds % 1000;
                sb.Append(microseconds);
                sb.Append('.');
                sb.Append((nanoseconds < 100) ? "0" : "");
                sb.Append((nanoseconds < 10) ? "0" : "");
                sb.Append(nanoseconds);
                sb.Append(" mcs");
            }
            else
            {
                sb.Append(nanoseconds);
                sb.Append(" ns");
            }

            return sb.ToString();
        }
    }

    public static class StringExtensions
    {
        private const int STABLE_HASH_CODE_SEED = 173;
        private const int STABLE_HASH_CODE_MUL = 37;

        private static string[] _queries =
        {
            "--", ";--", ";", "/*", "*/", "@@", "@", "char", "nchar", "varchar", "nvarchar", "alter", "begin", "cast",
            "create", "cursor", "declare","delete","drop", "end","exec", "execute", "fetch", "insert", "kill","select",
            "sys", "sysobjects","syscolumns", "table", "update"
        };

        public static bool ContainsSQL(this string src)
        {
            string rp = src.Replace("'", "''");
            foreach (var query in _queries)
            {
                if (rp.IndexOf(query, StringComparison.Ordinal) >= 0)
                {
                    return true;
                }
            }

            return false;
        }

        public static int GetStableHashCode(this string src, int offset, int length)
        {
            if (src == null)
            {
                throw new ArgumentNullException(nameof(src));
            }

            if (offset < 0 || offset >= src.Length)
            {
                throw new ArgumentException("Offset must be more than 0 and less than string length");
            }

            if (length < 0 || offset + length > src.Length)
            {
                throw new ArgumentException("Offset and length sum must be less than string length");
            }

            if (length == 0)
            {
                return 0;
            }

            int seed = STABLE_HASH_CODE_SEED;
            for (int i = offset, max = offset + length; i < max; i++)
            {
                seed = STABLE_HASH_CODE_MUL * seed * src[i];
            }

            return seed;
        }
    }
}