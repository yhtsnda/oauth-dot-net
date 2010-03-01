﻿// Copyright (c) 2008 Madgex
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// 
// OAuth.net uses the Common Service Locator interface, released under the MS-PL
// license. See "CommonServiceLocator License.txt" in the Licenses folder.
// 
// The examples and test cases use the Windsor Container from the Castle Project
// and Common Service Locator Windsor adaptor, released under the Apache License,
// Version 2.0. See "Castle Project License.txt" in the Licenses folder.
// 
// XRDS-Simple.net uses the HTMLAgility Pack. See "HTML Agility Pack License.txt"
// in the Licenses folder.
//
// Authors: Bruce Boughton, Chris Adams
// Website: http://lab.madgex.com/oauth-net/
// Email:   oauth-dot-net@madgex.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;

namespace OAuth.Net.Examples.TwitterClient.Api
{
    [XmlType(TypeName = "status")]
    public class Status
    {
        [XmlElement(ElementName = "created_at")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores",
            Justification = "Shim for XML serialization. Not intended to be used by callers")]
        public string __CreatedDate
        {
            get
            {
                return this.CreatedDate.ToString(
                    TwitterApi.DateFormat, 
                    CultureInfo.InvariantCulture);
            }

            set
            {
                this.CreatedDate = DateTime
                    .ParseExact(value, TwitterApi.DateFormat, CultureInfo.InvariantCulture);
            }
        }

        [XmlIgnore]
        public DateTime CreatedDate { get; set; }

        [XmlElement(ElementName = "id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "text")]
        public string Text { get; set; }

        [XmlElement(ElementName = "source")]
        public string Source { get; set; }

        [XmlElement(ElementName = "truncated")]
        public bool IsTruncated { get; set; }

        [XmlElement(ElementName = "in_reply_to_status_id")]
        public string InReplyToStatusId { get; set; }

        [XmlElement(ElementName = "in_reply_to_user_id")]
        public string InReplyToUserId { get; set; }

        [XmlElement(ElementName = "favorited")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", 
            MessageId = "Favorited", Justification = "Matches Twitter API")]
        public bool IsFavorited { get; set; }

        [XmlElement(ElementName = "user")]
        public BasicUser User { get; set; }

        /// <summary>
        /// Deserializes a Status from a stream.
        /// </summary>
        /// <param name="stream">Stream to deserialize from</param>
        /// <returns>Deserialized Status</returns>
        public static Status Deserialize(Stream stream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Status));
            return (Status)serializer.Deserialize(stream);
        }
    }
}