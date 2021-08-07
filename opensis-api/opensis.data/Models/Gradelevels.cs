﻿/***********************************************************************************
openSIS is a free student information system for public and non-public
schools from Open Solutions for Education, Inc.Website: www.os4ed.com.

Visit the openSIS product website at https://opensis.com to learn more.
If you have question regarding this software or the license, please contact
via the website.

The software is released under the terms of the GNU Affero General Public License as
published by the Free Software Foundation, version 3 of the License.
See https://www.gnu.org/licenses/agpl-3.0.en.html.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

Copyright (c) Open Solutions for Education, Inc.

All rights reserved.
***********************************************************************************/

using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class Gradelevels
    {
        public Gradelevels()
        {
            StudentEnrollment = new HashSet<StudentEnrollment>();
        }

        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int GradeId { get; set; }
        public string ShortName { get; set; }
        public string Title { get; set; }
        public int? EquivalencyId { get; set; }
        public int? AgeRangeId { get; set; }
        public int? IscedCode { get; set; }
        public int? NextGradeId { get; set; }
        public int? SortOrder { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }

        public virtual GradeAgeRange AgeRange { get; set; }
        public virtual GradeEquivalency Equivalency { get; set; }
        public virtual GradeEducationalStage IscedCodeNavigation { get; set; }
        public virtual SchoolMaster SchoolMaster { get; set; }
        public virtual ICollection<StudentEnrollment> StudentEnrollment { get; set; }

    }
}
