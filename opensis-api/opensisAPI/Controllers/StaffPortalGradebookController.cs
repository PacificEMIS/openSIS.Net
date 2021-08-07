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
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using opensis.core.StaffPortalGradebook.Interfaces;
using opensis.data.ViewModels.StaffPortalGradebook;

namespace opensisAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("{tenant}/StaffPortalGradebook")]
    [ApiController]
    public class StaffPortalGradebookController : ControllerBase
    {
        private IStaffPortalGradebookServices _staffPortalGradebookServices;
        public StaffPortalGradebookController(IStaffPortalGradebookServices staffPortalGradebookServices)
        {
            _staffPortalGradebookServices = staffPortalGradebookServices;
        }

        [HttpPost("addUpdateGradebookConfiguration")]
        public ActionResult<GradebookConfigurationAddViewModel> AddUpdateGradebookConfiguration(GradebookConfigurationAddViewModel gradebookConfigurationAddViewModel)
        {
            GradebookConfigurationAddViewModel gradebookConfigurationAdd = new GradebookConfigurationAddViewModel();
            try
            {
                gradebookConfigurationAdd = _staffPortalGradebookServices.AddUpdateGradebookConfiguration(gradebookConfigurationAddViewModel);
            }
            catch (Exception ex)
            {

                gradebookConfigurationAdd._message = ex.Message;
                gradebookConfigurationAdd._failure = true;
            }
            return gradebookConfigurationAdd;
        }

        [HttpPost("viewGradebookConfiguration")]
        public ActionResult<GradebookConfigurationAddViewModel> ViewGradebookConfiguration(GradebookConfigurationAddViewModel gradebookConfigurationAddViewModel)
        {
            GradebookConfigurationAddViewModel gradebookConfigurationView = new GradebookConfigurationAddViewModel();
            try
            {
                gradebookConfigurationView = _staffPortalGradebookServices.ViewGradebookConfiguration(gradebookConfigurationAddViewModel);
            }
            catch (Exception ex)
            {

                gradebookConfigurationView._message = ex.Message;
                gradebookConfigurationView._failure = true;
            }
            return gradebookConfigurationView;
        }

        [HttpPost("populateFinalGrading")]
        public ActionResult<FinalGradingMarkingPeriodList> PopulateFinalGrading(FinalGradingMarkingPeriodList finalGradingMarkingPeriodList)
        {
            FinalGradingMarkingPeriodList finalGradingMarkingPeriod = new FinalGradingMarkingPeriodList();
            try
            {
                finalGradingMarkingPeriod = _staffPortalGradebookServices.PopulateFinalGrading(finalGradingMarkingPeriodList);
            }
            catch (Exception ex)
            {

                finalGradingMarkingPeriod._message = ex.Message;
                finalGradingMarkingPeriod._failure = true;
            }
            return finalGradingMarkingPeriod;
        }
    }
}
