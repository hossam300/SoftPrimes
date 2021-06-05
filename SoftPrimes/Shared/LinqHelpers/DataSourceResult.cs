﻿using System.Collections.Generic;

namespace LinqHelper
{
    /// <summary>
    /// Describes the result of DataSource read operation.
    /// </summary>
    /// <typeparam name="T">The element type of the array</typeparam>
    public class DataSourceResult<T>
    {
        /// <summary>
        /// Represents a single page of processed data.
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// The total number of records available.
        /// </summary>
        public long Count { get; set; }
        public long CountUnReaded { get; set; }
        public string qrCodeType { get; set; }
    }
}