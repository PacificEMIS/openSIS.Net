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

using opensis.data.Interface;
using opensis.data.Models;
using opensis.data.ViewModels.ReportCard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using opensis.data.ViewModels.CourseManager;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading.Tasks;
using JSReport;
using opensis.data.Helper;
using System.Runtime.InteropServices;

namespace opensis.data.Repository
{
    public class ReportCardRepository : IReportCardRepository
    {
        private readonly CRMContext? context;
        private static readonly string NORECORDFOUND = "No Record Found";
        public ReportCardRepository(IDbContextFactory dbContextFactory)
        {
            this.context = dbContextFactory.Create();
        }
        /// <summary>
        /// Add Report Card Comments
        /// </summary>
        /// <param name="reportCardViewModel"></param>
        /// <returns></returns>

        //public ReportCardCommentsAddViewModel AddReportCardComments(ReportCardCommentsAddViewModel reportCardCommentsAddViewModel)
        //{
        //    using (var transaction = this.context.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            List<ReportCardComments> reportCardComments = new List<ReportCardComments>();

        //            int? id = 1;
        //            int? SortOrder = 1;

        //            if (reportCardCommentsAddViewModel.reportCardComments.Count > 0)
        //            {
        //                //var reportCardCommentData = this.context?.ReportCardComments.Where(x => x.TenantId == reportCardCommentsAddViewModel.TenantId && x.SchoolId == reportCardCommentsAddViewModel.SchoolId).OrderByDescending(x => x.Id).FirstOrDefault();

        //                //if (reportCardCommentData != null)
        //                //{
        //                //    id = Convert.ToInt32(reportCardCommentData.Id) + 1;
        //                //}

        //                //var reportCardCommentSortOrder = this.context?.ReportCardComments.Where(x => x.TenantId == reportCardCommentsAddViewModel.TenantId && x.SchoolId == reportCardCommentsAddViewModel.SchoolId && x.CourseCommentId == reportCardCommentsAddViewModel.CourseCommentId).OrderByDescending(x => x.Id).FirstOrDefault();

        //                //if (reportCardCommentSortOrder != null)
        //                //{
        //                //    SortOrder = reportCardCommentSortOrder.SortOrder + 1;
        //                //}

        //                //foreach (var reportComment in reportCardCommentsAddViewModel.reportCardComments.ToList())
        //                //{

        //                //    reportComment.TenantId = reportCardCommentsAddViewModel.TenantId;
        //                //    reportComment.SchoolId = reportCardCommentsAddViewModel.SchoolId;
        //                //    reportComment.CourseCommentId = reportCardCommentsAddViewModel.CourseCommentId;
        //                //    reportComment.CourseId = 1;
        //                //    reportComment.CourseSectionId = 1;
        //                //    reportComment.SortOrder = SortOrder;
        //                //    reportComment.Id = id;
        //                //    reportComment.CreatedOn = DateTime.UtcNow;
        //                //    reportCardComments.Add(reportComment);
        //                //    id++;
        //                //    SortOrder++;
        //                //}
        //                //reportCardCommentsAddViewModel._message = "Report Comments added succsesfully.";

        //                //this.context?.ReportCardComments.AddRange(reportCardComments);
        //                //this.context?.SaveChanges();
        //                //transaction.Commit();
        //                //reportCardCommentsAddViewModel._failure = false;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            transaction.Rollback();
        //            reportCardCommentsAddViewModel._failure = true;
        //            reportCardCommentsAddViewModel._message = ex.Message;
        //        }
        //        return reportCardCommentsAddViewModel;
        //    }
        //}

        ///// <summary>
        ///// Update Report Card Comments
        ///// </summary>
        ///// <param name="reportCardViewModel"></param>
        ///// <returns></returns>

        //public ReportCardCommentsAddViewModel UpdateReportCardComments(ReportCardCommentsAddViewModel reportCardCommentsAddViewModel)
        //{
        //    try
        //    {
        //        List<ReportCardComments> reportCardComments = new List<ReportCardComments>();

        //        if (reportCardCommentsAddViewModel.reportCardComments.Count > 0)
        //        {
        //           /* var reportCommentDataExist = this.context?.ReportCardComments.Where(x => x.TenantId == reportCardCommentsAddViewModel.TenantId && x.SchoolId == reportCardCommentsAddViewModel.SchoolId && x.CourseCommentId == reportCardCommentsAddViewModel.CourseCommentId).ToList();

        //            if (reportCommentDataExist.Count > 0)
        //            {
        //                this.context?.ReportCardComments.RemoveRange(reportCommentDataExist);
        //                this.context?.SaveChanges();
        //            }

        //            int? id = 1;
        //            int? SortOrder = 1;

        //            var reportCardCommentData = this.context?.ReportCardComments.Where(x => x.TenantId == reportCardCommentsAddViewModel.TenantId && x.SchoolId == reportCardCommentsAddViewModel.SchoolId).OrderByDescending(x => x.Id).FirstOrDefault();

        //            if (reportCardCommentData != null)
        //            {
        //                id = Convert.ToInt32(reportCardCommentData.Id) + 1;
        //            }

        //            var reportCardCommentSortOrder = this.context?.ReportCardComments.Where(x => x.TenantId == reportCardCommentsAddViewModel.TenantId && x.SchoolId == reportCardCommentsAddViewModel.SchoolId && x.CourseCommentId == reportCardCommentsAddViewModel.CourseCommentId).OrderByDescending(x => x.SortOrder).FirstOrDefault();

        //            if (reportCardCommentSortOrder != null)
        //            {
        //                SortOrder = reportCardCommentSortOrder.SortOrder + 1;
        //            }

        //            foreach (var reportComment in reportCardCommentsAddViewModel.reportCardComments.ToList())
        //            {

        //                reportComment.TenantId = reportCardCommentsAddViewModel.TenantId;
        //                reportComment.SchoolId = reportCardCommentsAddViewModel.SchoolId;
        //                reportComment.CourseCommentId = reportCardCommentsAddViewModel.CourseCommentId;
        //                reportComment.CourseId = 1;
        //                reportComment.CourseSectionId = 1;

        //                reportComment.SortOrder = SortOrder;
        //                reportComment.Id = id;
        //                reportComment.UpdatedOn = DateTime.UtcNow;
        //                reportCardComments.Add(reportComment);
        //                id++;
        //                SortOrder++;
        //            }
        //            reportCardCommentsAddViewModel._message = "Report Comments Updated succsesfully.";

        //            this.context?.ReportCardComments.AddRange(reportCardComments);
        //            this.context?.SaveChanges();
        //            //transaction.Commit();
        //            reportCardCommentsAddViewModel._failure = false;*/
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //transaction.Rollback();
        //        reportCardCommentsAddViewModel._failure = true;
        //        reportCardCommentsAddViewModel._message = ex.Message;
        //    }
        //    return reportCardCommentsAddViewModel;
        //}

        ///// <summary>
        ///// Delete Report Card Comments
        ///// </summary>
        ///// <param name="reportCardViewModel"></param>
        ///// <returns></returns>
        //public ReportCardCommentsAddViewModel DeleteReportCardComments(ReportCardCommentsAddViewModel reportCardCommentsAddViewModel)
        //{
        //    try
        //    {
        //       /* if (reportCardCommentsAddViewModel.reportCardComments.Count > 0)
        //        {
        //            var reportCardCommentDataExist = this.context?.ReportCardComments.FirstOrDefault(x => x.TenantId == reportCardCommentsAddViewModel.TenantId && x.SchoolId == reportCardCommentsAddViewModel.SchoolId && x.Id == reportCardCommentsAddViewModel.reportCardComments.FirstOrDefault().Id && x.CourseCommentId == reportCardCommentsAddViewModel.CourseCommentId);

        //            if (reportCardCommentDataExist != null)
        //            {
        //                this.context?.ReportCardComments.Remove(reportCardCommentDataExist);
        //                this.context?.SaveChanges();
        //                reportCardCommentsAddViewModel._failure = false;
        //                reportCardCommentsAddViewModel._message = "Deleted Successfully";
        //            }
        //            else
        //            {
        //                reportCardCommentsAddViewModel._failure = true;
        //                reportCardCommentsAddViewModel._message = NORECORDFOUND;
        //            }
        //        }*/
        //    }
        //    catch (Exception ex)
        //    {
        //        reportCardCommentsAddViewModel._failure = true;
        //        reportCardCommentsAddViewModel._message = ex.Message;
        //    }
        //    return reportCardCommentsAddViewModel;
        //}


        /// <summary>
        /// Add Course Comment Category
        /// </summary>
        /// <param name="courseCommentCategoryAddViewModel"></param>
        /// <returns></returns>
        //public CourseCommentCategoryAddViewModel AddCourseCommentCategory(CourseCommentCategoryAddViewModel courseCommentCategoryAddViewModel)
        //{
        //    try
        //    {
        //        List<CourseCommentCategory> courseCommentCategoryList = new List<CourseCommentCategory>();
        //        int i = 0;
        //        var distinctCourseData = courseCommentCategoryAddViewModel.courseCommentCategory.Select(s => new { s.CourseId, s.TenantId, s.SchoolId }).Distinct().ToList();

        //        int? courseCommentId = 1;

        //        foreach (var course in distinctCourseData.ToList())
        //        {
        //            var courseCommentCategoryDataExist = this.context?.CourseCommentCategory.Where(x => x.TenantId == course.TenantId && x.SchoolId == course.SchoolId && x.CourseId == course.CourseId).ToList();

        //            if (courseCommentCategoryDataExist != null && courseCommentCategoryDataExist.Any())
        //            {
        //                this.context?.CourseCommentCategory.RemoveRange(courseCommentCategoryDataExist);
        //                this.context?.SaveChanges();
        //            }

        //            var courseCommentCategoryData = courseCommentCategoryAddViewModel.courseCommentCategory.Where(x => x.CourseId == course.CourseId).ToList();

        //            int? sortOrder = 1;
        //            int? sortOrderForAllCourse = 1;

        //            if (i == 0)
        //            {
        //                var courseCommentCategoryDataForID = this.context?.CourseCommentCategory.Where(x => x.TenantId == course.TenantId && x.SchoolId == course.SchoolId).OrderByDescending(x => x.CourseCommentId).FirstOrDefault();

        //                if (courseCommentCategoryDataForID != null)
        //                {
        //                    courseCommentId = courseCommentCategoryDataForID.CourseCommentId + 1;
        //                }
        //            }

        //            foreach (var courseCommentCategory in courseCommentCategoryData)
        //            {
        //                courseCommentCategory.CourseCommentId = (int)courseCommentId;
        //                courseCommentCategory.SortOrder = courseCommentCategory.CourseId != null ? sortOrder : sortOrderForAllCourse;
        //                courseCommentCategory.CreatedOn = DateTime.UtcNow;
        //                courseCommentCategoryList.Add(courseCommentCategory);
        //                courseCommentId++;
        //                sortOrder++;
        //                sortOrderForAllCourse++;
        //            }
        //            i++;
        //        }

        //        this.context?.CourseCommentCategory.AddRange(courseCommentCategoryList);
        //        this.context?.SaveChanges();
        //        courseCommentCategoryAddViewModel._failure = false;
        //        courseCommentCategoryAddViewModel._message = "Course Comment Category Added Successfully";

        //    }
        //    catch (Exception es)
        //    {
        //        courseCommentCategoryAddViewModel._message = es.Message;
        //        courseCommentCategoryAddViewModel._failure = true;
        //    }
        //    return courseCommentCategoryAddViewModel;
        //}
        public CourseCommentCategoryAddViewModel AddCourseCommentCategory(CourseCommentCategoryAddViewModel courseCommentCategoryAddViewModel)
        {
            if (courseCommentCategoryAddViewModel.courseCommentCategory.Any() == false)
            {
                return courseCommentCategoryAddViewModel;
            }

            try
            {
                List<CourseCommentCategory> courseCommentCategoryList = new List<CourseCommentCategory>();
                int i = 0;
                var distinctCourseData = courseCommentCategoryAddViewModel.courseCommentCategory.Select(s => new { s.CourseId, s.TenantId, s.SchoolId }).Distinct().ToList();

                int? courseCommentId = 1;

                decimal? academicYear = Utility.GetCurrentAcademicYear(this.context!, courseCommentCategoryAddViewModel.courseCommentCategory.FirstOrDefault()!.TenantId, courseCommentCategoryAddViewModel.courseCommentCategory.FirstOrDefault()!.SchoolId);

                foreach (var course in distinctCourseData.ToList())
                {
                    int? sortOrder = 1;
                    var courseCommentCategoryDataForSortOrder = this.context?.CourseCommentCategory.Where(x => x.TenantId == course.TenantId && x.SchoolId == course.SchoolId && x.CourseId == course.CourseId).OrderByDescending(x => x.SortOrder).FirstOrDefault();
                    if (courseCommentCategoryDataForSortOrder != null)
                    {
                        sortOrder = courseCommentCategoryDataForSortOrder.SortOrder + 1;
                    }

                    var courseCommentCategoryData = courseCommentCategoryAddViewModel.courseCommentCategory.Where(x => x.CourseId == course.CourseId).ToList();

                    if (i == 0)
                    {
                        var courseCommentCategoryDataForID = this.context?.CourseCommentCategory.Where(x => x.TenantId == course.TenantId && x.SchoolId == course.SchoolId).OrderByDescending(x => x.CourseCommentId).FirstOrDefault();

                        if (courseCommentCategoryDataForID != null)
                        {
                            courseCommentId = courseCommentCategoryDataForID.CourseCommentId + 1;
                        }
                    }

                    foreach (var courseCommentCategory in courseCommentCategoryData)
                    {
                        if (courseCommentCategory.CourseCommentId > 0)
                        {
                            var courseCommentData = this.context?.CourseCommentCategory.FirstOrDefault(x => x.TenantId == courseCommentCategory.TenantId && x.SchoolId == courseCommentCategory.SchoolId && x.CourseId == courseCommentCategory.CourseId && x.CourseCommentId == courseCommentCategory.CourseCommentId);

                            if (courseCommentData != null)
                            {
                                courseCommentData.Comments = courseCommentCategory.Comments;
                                courseCommentData.UpdatedBy = courseCommentCategory.UpdatedBy;
                                courseCommentData.UpdatedOn = DateTime.UtcNow;
                            }        
                        }
                        else
                        {
                            courseCommentCategory.AcademicYear = academicYear;
                            courseCommentCategory.CourseCommentId = (int)courseCommentId;
                            courseCommentCategory.SortOrder = sortOrder;
                            courseCommentCategory.CreatedOn = DateTime.UtcNow;
                            courseCommentCategoryList.Add(courseCommentCategory);
                            courseCommentId++;
                            sortOrder++;
                        }
                    }
                    i++;
                }
                this.context?.CourseCommentCategory.AddRange(courseCommentCategoryList);
                this.context?.SaveChanges();
                courseCommentCategoryAddViewModel._failure = false;
                courseCommentCategoryAddViewModel._message = "Course Comment Category Added Successfully";
            }
            catch (Exception es)
            {
                courseCommentCategoryAddViewModel._message = es.Message;
                courseCommentCategoryAddViewModel._failure = true;
            }
            return courseCommentCategoryAddViewModel;
        }

        /// <summary>
        /// Delete Course Comment Category
        /// </summary>
        /// <param name="courseCommentCategoryDeleteViewModel"></param>
        /// <returns></returns>
        public CourseCommentCategoryDeleteViewModel DeleteCourseCommentCategory(CourseCommentCategoryDeleteViewModel courseCommentCategoryDeleteViewModel)
        {
            try
            {
                if (courseCommentCategoryDeleteViewModel.CourseCommentId != null)
                {
                    var courseCommentData = this.context?.CourseCommentCategory.Include(x => x.StudentFinalGradeComments).Where(x => x.TenantId == courseCommentCategoryDeleteViewModel.TenantId && x.SchoolId == courseCommentCategoryDeleteViewModel.SchoolId && x.CourseId == courseCommentCategoryDeleteViewModel.CourseId && x.CourseCommentId == courseCommentCategoryDeleteViewModel.CourseCommentId).FirstOrDefault();

                    if (courseCommentData != null)
                    {
                        if (courseCommentData.StudentFinalGradeComments.Any())
                        {
                            courseCommentCategoryDeleteViewModel._failure = true;
                            courseCommentCategoryDeleteViewModel._message = "Course Comments cannot be deleted because it has its association";
                        }
                        else
                        {
                            this.context?.CourseCommentCategory.Remove(courseCommentData);
                            this.context?.SaveChanges();
                            courseCommentCategoryDeleteViewModel._failure = false;
                            courseCommentCategoryDeleteViewModel._message = "Course Comments Deleted Successfully";
                        }

                    }
                }
                else
                {
                    var courseComments = this.context?.CourseCommentCategory.Include(x => x.StudentFinalGradeComments).Where(x => x.TenantId == courseCommentCategoryDeleteViewModel.TenantId && x.SchoolId == courseCommentCategoryDeleteViewModel.SchoolId && x.CourseId == courseCommentCategoryDeleteViewModel.CourseId).ToList();

                    if (courseComments != null && courseComments.Any())
                    {
                        var checkChild = courseComments.Where(x => x.StudentFinalGradeComments.Count > 0);
                        if (checkChild != null && checkChild.Any())
                        {
                            courseCommentCategoryDeleteViewModel._failure = true;
                            courseCommentCategoryDeleteViewModel._message = "Course Comments cannot be deleted because it has its association";
                        }
                        else
                        {
                            this.context?.CourseCommentCategory.RemoveRange(courseComments);
                            this.context?.SaveChanges();
                            courseCommentCategoryDeleteViewModel._failure = false;
                            courseCommentCategoryDeleteViewModel._message = "Course Comments Deleted Successfully";
                        }
                    }
                }
            }

            //var courseCommentData = this.context?.CourseCommentCategory.Include(x => x.StudentFinalGradeComments).Where(x => x.TenantId == courseCommentCategoryDeleteViewModel.TenantId && x.SchoolId == courseCommentCategoryDeleteViewModel.SchoolId && x.CourseId == courseCommentCategoryDeleteViewModel.CourseId && x.CourseCommentId == courseCommentCategoryDeleteViewModel.CourseCommentId).FirstOrDefault();

            //if (courseCommentData != null)
            //{
            //    if (courseCommentData.StudentFinalGradeComments.Any())
            //    {
            //        courseCommentCategoryDeleteViewModel._failure = true;
            //        courseCommentCategoryDeleteViewModel._message = "Course Comments cannot be deleted because it has its association";
            //    }
            //    else
            //    {
            //        this.context?.CourseCommentCategory.Remove(courseCommentData);
            //        this.context?.SaveChanges();
            //        courseCommentCategoryDeleteViewModel._failure = false;
            //        courseCommentCategoryDeleteViewModel._message = "Course Comments Deleted Successfully";
            //    }
            //}
            //else
            //{
            //    courseCommentCategoryDeleteViewModel._failure = true;
            //    courseCommentCategoryDeleteViewModel._message = NORECORDFOUND;
            //}

            catch (Exception es)
            {
                courseCommentCategoryDeleteViewModel._failure = true;
                courseCommentCategoryDeleteViewModel._message = es.Message;
            }
            return courseCommentCategoryDeleteViewModel;
        }

        /// <summary>
        /// Update Sort Order For Course Comment Category
        /// </summary>
        /// <param name="courseCommentCategorySortOrderViewModel"></param>
        /// <returns></returns>
        public CourseCommentCategorySortOrderViewModel UpdateSortOrderForCourseCommentCategory(CourseCommentCategorySortOrderViewModel courseCommentCategorySortOrderViewModel)
        {
            try
            {
                //if (courseCommentCategorySortOrderViewModel.CourseId > 0)
                //{
                    var SortOrderItem = new List<CourseCommentCategory>();

                    var targetSortOrderItem = this.context?.CourseCommentCategory.FirstOrDefault(x => x.SortOrder == courseCommentCategorySortOrderViewModel.PreviousSortOrder && x.SchoolId == courseCommentCategorySortOrderViewModel.SchoolId && x.TenantId == courseCommentCategorySortOrderViewModel.TenantId && x.CourseId == courseCommentCategorySortOrderViewModel.CourseId);

                    if (targetSortOrderItem != null)
                    {
                        targetSortOrderItem.SortOrder = courseCommentCategorySortOrderViewModel.CurrentSortOrder;
                        targetSortOrderItem.UpdatedBy = courseCommentCategorySortOrderViewModel.UpdatedBy;
                        targetSortOrderItem.UpdatedOn = DateTime.UtcNow;

                    if (courseCommentCategorySortOrderViewModel.PreviousSortOrder > courseCommentCategorySortOrderViewModel.CurrentSortOrder)
                        {
                            SortOrderItem = this.context?.CourseCommentCategory.Where(x => x.SortOrder >= courseCommentCategorySortOrderViewModel.CurrentSortOrder && x.SortOrder < courseCommentCategorySortOrderViewModel.PreviousSortOrder && x.SchoolId == courseCommentCategorySortOrderViewModel.SchoolId && x.TenantId == courseCommentCategorySortOrderViewModel.TenantId && x.CourseId == courseCommentCategorySortOrderViewModel.CourseId).ToList();

                            if (SortOrderItem != null && SortOrderItem.Any())
                            {
                                SortOrderItem.ForEach(x => { x.SortOrder = x.SortOrder + 1; x.UpdatedOn = DateTime.UtcNow; x.UpdatedBy = courseCommentCategorySortOrderViewModel.UpdatedBy; });
                            }
                        }

                        if (courseCommentCategorySortOrderViewModel.CurrentSortOrder > courseCommentCategorySortOrderViewModel.PreviousSortOrder)
                        {
                            SortOrderItem = this.context?.CourseCommentCategory.Where(x => x.SortOrder <= courseCommentCategorySortOrderViewModel.CurrentSortOrder && x.SortOrder > courseCommentCategorySortOrderViewModel.PreviousSortOrder && x.SchoolId == courseCommentCategorySortOrderViewModel.SchoolId && x.TenantId == courseCommentCategorySortOrderViewModel.TenantId && x.CourseId == courseCommentCategorySortOrderViewModel.CourseId).ToList();

                        if (SortOrderItem != null && SortOrderItem.Any())
                            {
                                SortOrderItem.ForEach(x => { x.SortOrder = x.SortOrder - 1; ; x.UpdatedOn = DateTime.UtcNow; x.UpdatedBy = courseCommentCategorySortOrderViewModel.UpdatedBy; });
                            }
                        }
                    }
                    this.context?.SaveChanges();
                    courseCommentCategorySortOrderViewModel._failure = false;
                //}
            }
            catch (Exception es)
            {
                courseCommentCategorySortOrderViewModel._message = es.Message;
                courseCommentCategorySortOrderViewModel._failure = true;
            }
            return courseCommentCategorySortOrderViewModel;
        }
         
        /// <summary>
        /// Get All Course Comment Category With Report Card Comments
        /// </summary>
        /// <param name="courseCommentCategoryListViewModel"></param>
        /// <returns></returns>
        public CourseCommentCategoryListViewModel GetAllCourseCommentCategory(CourseCommentCategoryListViewModel courseCommentCategoryListViewModel)
        {
            CourseCommentCategoryListViewModel courseCommentCategoryList = new CourseCommentCategoryListViewModel();
            try
            {
                courseCommentCategoryList.TenantId = courseCommentCategoryListViewModel.TenantId;
                courseCommentCategoryList.SchoolId = courseCommentCategoryListViewModel.SchoolId;
                courseCommentCategoryList._tenantName = courseCommentCategoryListViewModel._tenantName;
                courseCommentCategoryList._token = courseCommentCategoryListViewModel._token;
                courseCommentCategoryList._userName = courseCommentCategoryListViewModel._userName;

                var courseCommentCategoryData = this.context?.CourseCommentCategory.Where(x => x.TenantId == courseCommentCategoryListViewModel.TenantId && x.SchoolId == courseCommentCategoryListViewModel.SchoolId && x.AcademicYear== courseCommentCategoryListViewModel.AcademicYear).ToList();

                if (courseCommentCategoryData != null && courseCommentCategoryData.Any())
                {
                    if (courseCommentCategoryListViewModel.IsListView == true)
                    {
                        courseCommentCategoryData.ForEach(c =>
                        {
                            c.CreatedBy = Utility.CreatedOrUpdatedBy(this.context, courseCommentCategoryListViewModel.TenantId, c.CreatedBy);
                            c.UpdatedBy = Utility.CreatedOrUpdatedBy(this.context, courseCommentCategoryListViewModel.TenantId, c.UpdatedBy);
                        });
                    }

                    courseCommentCategoryList.courseCommentCategories = courseCommentCategoryData;
                    courseCommentCategoryList._failure = false;
                }
                else
                {
                    courseCommentCategoryList._message = NORECORDFOUND;
                    courseCommentCategoryList._failure = true;
                }
            }
            catch (Exception es)
            {
                courseCommentCategoryList.courseCommentCategories = null!;
                courseCommentCategoryList._message = es.Message;
                courseCommentCategoryList._failure = true;
            }
            return courseCommentCategoryList;
        }

        /// <summary>
        /// Add Report Card
        /// </summary>
        /// <param name="reportCardViewModel"></param>
        /// <returns></returns>
        public ReportCardViewModel AddReportCard(ReportCardViewModel reportCardViewModel)
        {
            if (reportCardViewModel.MarkingPeriods is null)
            {
                return reportCardViewModel;
            }
            try
            {
                List<string> teacherComments = new List<string>();

                int i = 0;
                long? ide = 1;
                int teacherCommentsNo = 1;
                if (reportCardViewModel.studentsReportCardViewModelList.Count > 0)
                {
                    reportCardViewModel.AcademicYear= Utility.GetCurrentAcademicYear(this.context!, reportCardViewModel.TenantId, reportCardViewModel.SchoolId);

                    if (reportCardViewModel.TemplateType?.ToLower() == "default")
                    {

                        foreach (var student in reportCardViewModel.studentsReportCardViewModelList)
                        {
                            List<StudentReportCardMaster> studentReportCardMasterList = new List<StudentReportCardMaster>();
                            List<StudentReportCardDetail> studentReportCardDetailList = new List<StudentReportCardDetail>();

                            var existingStudentReportCardData = this.context?.StudentReportCardMaster.Where(x => x.SchoolId == reportCardViewModel.SchoolId && x.TenantId == reportCardViewModel.TenantId && x.StudentId == student.StudentId && x.SchoolYear == reportCardViewModel.AcademicYear.ToString()).ToList();

                            if (existingStudentReportCardData != null)
                            {
                                var existingStudentReportCardDetailsData = this.context?.StudentReportCardDetail.Where(x => x.SchoolId == reportCardViewModel.SchoolId && x.TenantId == reportCardViewModel.TenantId && x.StudentId == student.StudentId && x.SchoolYear == reportCardViewModel.AcademicYear.ToString()).ToList();
                                if (existingStudentReportCardDetailsData != null && existingStudentReportCardDetailsData.Any())
                                {
                                    this.context?.StudentReportCardDetail.RemoveRange(existingStudentReportCardDetailsData);
                                }
                                this.context?.StudentReportCardMaster.RemoveRange(existingStudentReportCardData);
                                this.context?.SaveChanges();
                            }
                            if (i == 0)
                            {
                                var idData = this.context?.StudentReportCardDetail.Where(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId).OrderByDescending(x => x.Id).FirstOrDefault();

                                if (idData != null)
                                {
                                    ide = idData.Id + 1;
                                }
                            }

                            int? absencesInDays = 0;

                            var studentData = this.context?.StudentMaster.Include(x => x.StudentEnrollment).FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.StudentId == student.StudentId);

                            //var GradeLevelTitle = studentData.StudentEnrollment.Where(x => x.IsActive == true).Select(s => s.GradeLevelTitle).FirstOrDefault();

                            var GradeLevelTitle = studentData!.StudentEnrollment.Where(x => x.IsActive == true).Select(s => s.GradeLevelTitle).FirstOrDefault();

                            var markingPeriodsData = reportCardViewModel.MarkingPeriods.Split(",");
                            DateTime? startDate = null;
                            DateTime? endDate = null;
                            string MarkingPeriodTitle = null!;

                            foreach (var markingPeriod in markingPeriodsData)
                            {
                                List<DateTime> holidayList = new List<DateTime>();
                                int? Absences = 0;
                                int? ExcusedAbsences = 0;
                                int? QtrMarkingPeriodId = null;
                                int? SmstrMarkingPeriodId = null;
                                int? YrMarkingPeriodId = null;
                                int? PrgrsprdMarkingPeriodId = null;

                                if (markingPeriod != null)
                                {
                                    var markingPeriodid = markingPeriod.Split("_", StringSplitOptions.RemoveEmptyEntries);

                                    if (markingPeriodid.First() == "3")
                                    {
                                        PrgrsprdMarkingPeriodId = Int32.Parse(markingPeriodid.ElementAt(1));

                                        var ppData = this.context?.ProgressPeriods.FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.MarkingPeriodId == PrgrsprdMarkingPeriodId && x.AcademicYear == reportCardViewModel.AcademicYear);

                                        //MarkingPeriodTitle = qtrData.Title;
                                        MarkingPeriodTitle = ppData!.Title!;
                                        startDate = ppData.StartDate;
                                        endDate = ppData.EndDate;
                                    }

                                    if (markingPeriodid.First() == "2")
                                    {
                                        QtrMarkingPeriodId = Int32.Parse(markingPeriodid.ElementAt(1));

                                        var qtrData = this.context?.Quarters.FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.MarkingPeriodId == QtrMarkingPeriodId && x.AcademicYear == reportCardViewModel.AcademicYear);

                                        //MarkingPeriodTitle = qtrData.Title;
                                        MarkingPeriodTitle = qtrData!.Title!;
                                        startDate = qtrData.StartDate;
                                        endDate = qtrData.EndDate;
                                    }

                                    if (markingPeriodid.First() == "1")
                                    {
                                        SmstrMarkingPeriodId = Int32.Parse(markingPeriodid.ElementAt(1));

                                        var smstrData = this.context?.Semesters.FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.MarkingPeriodId == SmstrMarkingPeriodId && x.AcademicYear == reportCardViewModel.AcademicYear);

                                        //MarkingPeriodTitle = smstrData.Title;
                                        MarkingPeriodTitle = smstrData!.Title!;
                                        startDate = smstrData.StartDate;
                                        endDate = smstrData.EndDate;
                                    }

                                    if (markingPeriodid.First() == "0")
                                    {
                                        YrMarkingPeriodId = Int32.Parse(markingPeriodid.ElementAt(1));

                                        var yrData = this.context?.SchoolYears.FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.MarkingPeriodId == YrMarkingPeriodId && x.AcademicYear == reportCardViewModel.AcademicYear);

                                        //MarkingPeriodTitle = yrData.Title;
                                        MarkingPeriodTitle = yrData!.Title!;
                                        startDate = yrData.StartDate;
                                        endDate = yrData.EndDate;
                                    }

                                    var studentAttendanceData = this.context?.StudentAttendance.Include(x => x.AttendanceCodeNavigation).Where(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.StudentId == student.StudentId && x.AttendanceDate >= startDate && x.AttendanceDate <= endDate).ToList();

                                    if (studentAttendanceData != null && studentAttendanceData.Any())
                                    {
                                        Absences = studentAttendanceData.Where(x => x.AttendanceCodeNavigation.StateCode.ToLower() == "absent").Count();
                                        ExcusedAbsences = studentAttendanceData.Where(x => x.AttendanceCodeNavigation.StateCode.ToLower() == "excusedabsent").Count();
                                        var prasentData = studentAttendanceData.Where(x => x.AttendanceCodeNavigation.StateCode.ToLower() == "present");

                                        absencesInDays += Absences + ExcusedAbsences;
                                    }

                                    var reportCardData = this.context?.StudentFinalGrade.Include(x => x.StudentFinalGradeComments).Include(x => x.StudentFinalGradeStandard).Where(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.StudentId == student.StudentId && x.AcademicYear == reportCardViewModel.AcademicYear && ((x.YrMarkingPeriodId != null && x.YrMarkingPeriodId == YrMarkingPeriodId) || (x.SmstrMarkingPeriodId != null && x.SmstrMarkingPeriodId == SmstrMarkingPeriodId) || (x.QtrMarkingPeriodId != null && x.QtrMarkingPeriodId == QtrMarkingPeriodId) || (x.PrgrsprdMarkingPeriodId != null && x.PrgrsprdMarkingPeriodId == PrgrsprdMarkingPeriodId))).ToList();

                                    decimal? gPaValue = 0.0m;
                                    decimal? CreditEarned = 0.0m;
                                    decimal? CreditHours = 0.0m;


                                    if (reportCardData != null && reportCardData.Any())
                                    {
                                        foreach (var reportCard in reportCardData)
                                        {
                                            var CourseSectionData = this.context?.CourseSection.Include(x => x.StaffCoursesectionSchedule).ThenInclude(x => x.StaffMaster).FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.CourseSectionId == reportCard.CourseSectionId && x.CourseId == reportCard.CourseId);

                                            var gradeData = this.context?.Grade.FirstOrDefault(x => x.TenantId == reportCard.TenantId && x.SchoolId == reportCard.SchoolId && x.Title == reportCard.GradeObtained && x.GradeScaleId == reportCard.GradeScaleId);

                                            if (gradeData != null)
                                            {
                                                //CreditHours = CourseSectionData.CreditHours;
                                                CreditHours = CourseSectionData!.CreditHours;
                                                CreditEarned = reportCard.CreditEarned != null ? reportCard.CreditEarned : CourseSectionData.CreditHours;
                                                gPaValue = CourseSectionData.IsWeightedCourse != true ? gradeData.UnweightedGpValue * (CreditHours / CreditEarned) : gradeData.WeightedGpValue * (CreditHours / CreditEarned);

                                            }

                                            var comments = reportCard.StudentFinalGradeComments.Select(x => x.CourseCommentId).ToList();
                                            var Comments = string.Join(",", comments.Select(x => x.ToString()).ToArray());

                                            var studentReportCardDetail = new StudentReportCardDetail()
                                            {
                                                Id = (long)ide,
                                                TenantId = reportCardViewModel.TenantId,
                                                SchoolId = reportCardViewModel.SchoolId,
                                                StudentId = student.StudentId,
                                                SchoolYear = reportCardViewModel.AcademicYear.ToString()!,
                                                GradeTitle = GradeLevelTitle!,
                                                MarkingPeriodTitle = MarkingPeriodTitle,
                                                CourseName = CourseSectionData!.CourseSectionName,
                                                Teacher = reportCardViewModel.TeacherName == true ? CourseSectionData.StaffCoursesectionSchedule.Count > 0 ? CourseSectionData.StaffCoursesectionSchedule.FirstOrDefault()!.StaffMaster.FirstGivenName + " " + CourseSectionData.StaffCoursesectionSchedule.FirstOrDefault()!.StaffMaster.MiddleName + " " + CourseSectionData.StaffCoursesectionSchedule.FirstOrDefault()!.StaffMaster.LastFamilyName : null : null,
                                                Grade = reportCardViewModel.Parcentage != true ? reportCard.GradeObtained : reportCard.GradeObtained + "(" + reportCard.PercentMarks + ")",
                                                Gpa = reportCardViewModel.GPA == true ? gPaValue : null,
                                                Comments = Comments,
                                                TeacherComments = reportCardViewModel.TeacherComments == true ? reportCard.TeacherComment != null ? (teacherCommentsNo++).ToString() : null : null,
                                                OverallTeacherComments = reportCard.TeacherComment,

                                                CreatedBy = reportCardViewModel.CreatedBy,
                                                CreatedOn = DateTime.UtcNow

                                            };
                                            studentReportCardDetailList.Add(studentReportCardDetail);
                                            ide++;

                                        }
                                        this.context?.StudentReportCardDetail.AddRange(studentReportCardDetailList);
                                    }

                                    double attendencePercent = 0;
                                    int prasentDay = 0;

                                    var calenderData = this.context?.SchoolCalendars.FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.DefaultCalender == true && x.AcademicYear == reportCardViewModel.AcademicYear);

                                    var schoolYearData = this.context?.SchoolYears.FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.AcademicYear == reportCardViewModel.AcademicYear);

                                    if (calenderData != null && schoolYearData != null)
                                    { 
                                      
                                        DateTime schoolYearStartDate = (DateTime)schoolYearData.StartDate!;
                                        DateTime schoolYearEndDate = (DateTime)schoolYearData.EndDate!;

                                        // Calculate Holiday
                                         var CalendarEventsData = this.context?.CalendarEvents.Where(e => e.TenantId == reportCardViewModel.TenantId && e.AcademicYear == reportCardViewModel.AcademicYear && (e.StartDate >= schoolYearStartDate && e.StartDate <= schoolYearEndDate || e.EndDate >= schoolYearStartDate && e.EndDate <= schoolYearEndDate) && e.IsHoliday == true && (e.SchoolId == reportCardViewModel.SchoolId || e.ApplicableToAllSchool == true)).ToList();

                                        if (CalendarEventsData != null && CalendarEventsData.Any())
                                        {
                                            foreach (var calender in CalendarEventsData)
                                            {
                                                if (calender.EndDate!.Value.Date > calender.StartDate!.Value.Date)
                                                {
                                                    var date = Enumerable.Range(0, 1 + (calender.EndDate.Value.Date - calender.StartDate.Value.Date).Days)
                                                       .Select(i => calender.StartDate.Value.Date.AddDays(i))
                                                       .ToList();
                                                    holidayList.AddRange(date);
                                                }
                                                holidayList.Add(calender.StartDate.Value.Date);
                                            }
                                        }

                                        var daysValue = "0123456";
                                        var weekdays = calenderData.Days;
                                        //var WeekOffDays = Regex.Split(daysValue, weekdays);
                                        var WeekOffDays = Regex.Split(daysValue, weekdays!);
                                        var WeekOfflist = new List<string>();
                                        foreach (var WeekOffDay in WeekOffDays)
                                        {
                                            Days days = new Days();
                                            var Day = Enum.GetName(days.GetType(), Convert.ToInt32(WeekOffDay));
                                            //WeekOfflist.Add(Day);
                                            WeekOfflist.Add(Day!);
                                        }

                                        int workDays = 0;
                                        while (schoolYearStartDate != schoolYearEndDate)
                                        {
                                            if (!holidayList.Contains(schoolYearStartDate))
                                            {
                                                if (!WeekOfflist.Contains(schoolYearStartDate.DayOfWeek.ToString()))
                                                {
                                                    workDays++;
                                                }
                                                schoolYearStartDate = schoolYearStartDate.AddDays(1);
                                            }

                                        }

                                        var studentPrasentAttendanceData = this.context?.StudentAttendance.Include(x => x.AttendanceCodeNavigation).Where(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.StudentId == student.StudentId && x.AttendanceDate >= calenderData.StartDate && x.AttendanceDate <= calenderData.EndDate).ToList();

                                        if (studentPrasentAttendanceData != null && studentPrasentAttendanceData.Any())
                                        {
                                            prasentDay = studentPrasentAttendanceData.Where(x => x.AttendanceCodeNavigation.StateCode.ToLower() == "present").Count();
                                        }
                                        var presentDays = workDays - absencesInDays;
                                        attendencePercent = (double)(presentDays * 100 / workDays);
                                        //attendencePercent = ((presentDays / workDays) * 100);
                                    }

                                    var studentReportCardMaster = new StudentReportCardMaster
                                    {
                                        TenantId = reportCardViewModel.TenantId,
                                        SchoolId = reportCardViewModel.SchoolId,
                                        StudentId = student.StudentId,
                                        SchoolYear = reportCardViewModel.AcademicYear.ToString()!,
                                        GradeTitle = GradeLevelTitle!,
                                        StudentInternalId = studentData.StudentInternalId,
                                        YodAbsence = reportCardViewModel.YearToDateDailyAbsences == true ? absencesInDays : null,
                                        YodAttendance = reportCardViewModel.YearToDateDailyAbsences == true ? Math.Round(attendencePercent, 2).ToString() + "%" : null,
                                        ReportGenerationDate = DateTime.UtcNow,
                                        Absences = reportCardViewModel.DailyAbsencesThisMarkingPeriod == true ? Absences : null,
                                        ExcusedAbsences = reportCardViewModel.DailyAbsencesThisMarkingPeriod == true ? ExcusedAbsences : null,
                                        MarkingPeriodTitle = MarkingPeriodTitle,
                                        CreatedBy = reportCardViewModel.CreatedBy,
                                        CreatedOn = DateTime.UtcNow
                                    };
                                    studentReportCardMasterList.Add(studentReportCardMaster);
                                }
                            }
                            this.context?.StudentReportCardMaster.AddRange(studentReportCardMasterList);
                            i++;
                        }
                    }
                    else
                    {
                        foreach (var student in reportCardViewModel.studentsReportCardViewModelList)
                        {
                            List<StudentReportCardMaster> studentReportCardMasterList = new List<StudentReportCardMaster>();
                            List<StudentReportCardDetail> studentReportCardDetailList = new List<StudentReportCardDetail>();

                            var existingStudentReportCardData = this.context?.StudentReportCardMaster.Where(x => x.SchoolId == reportCardViewModel.SchoolId && x.TenantId == reportCardViewModel.TenantId && x.StudentId == student.StudentId && x.SchoolYear == reportCardViewModel.AcademicYear.ToString()).ToList();

                            if (existingStudentReportCardData != null && existingStudentReportCardData.Any())
                            {
                                var existingStudentReportCardDetailsData = this.context?.StudentReportCardDetail.Where(x => x.SchoolId == reportCardViewModel.SchoolId && x.TenantId == reportCardViewModel.TenantId && x.StudentId == student.StudentId && x.SchoolYear == reportCardViewModel.AcademicYear.ToString()).ToList();

                                if (existingStudentReportCardDetailsData != null && existingStudentReportCardDetailsData.Any())
                                {
                                    this.context?.StudentReportCardDetail.RemoveRange(existingStudentReportCardDetailsData);
                                }
                                this.context?.StudentReportCardMaster.RemoveRange(existingStudentReportCardData);
                                this.context?.SaveChanges();
                            }
                            if (i == 0)
                            {
                                var idData = this.context?.StudentReportCardDetail.Where(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId).OrderByDescending(x => x.Id).FirstOrDefault();

                                if (idData != null)
                                {
                                    ide = idData.Id + 1;
                                }
                            }

                            var studentData = this.context?.StudentMaster.Include(x => x.StudentEnrollment).FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.StudentId == student.StudentId);

                            //var GradeLevelTitle = studentData.StudentEnrollment.Where(x => x.IsActive == true).Select(s => s.GradeLevelTitle).FirstOrDefault();

                            var GradeLevelTitle = studentData!.StudentEnrollment.Where(x => x.IsActive == true).Select(s => s.GradeLevelTitle).FirstOrDefault();

                            var markingPeriodsData = reportCardViewModel.MarkingPeriods.Split(",");
                            DateTime? startDate = null;
                            DateTime? endDate = null;
                            string? MarkingPeriodTitle = null;

                            foreach (var markingPeriod in markingPeriodsData)
                            {
                                int? QtrMarkingPeriodId = null;
                                int? SmstrMarkingPeriodId = null;
                                int? YrMarkingPeriodId = null;
                                int? PrgrsprdMarkingPeriodId = null;

                                if (markingPeriod != null)
                                {
                                    var markingPeriodid = markingPeriod.Split("_", StringSplitOptions.RemoveEmptyEntries);

                                    if (markingPeriodid.First() == "3")
                                    {
                                        PrgrsprdMarkingPeriodId = Int32.Parse(markingPeriodid.ElementAt(1));

                                        var ppData = this.context?.ProgressPeriods.FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.MarkingPeriodId == PrgrsprdMarkingPeriodId && x.AcademicYear == reportCardViewModel.AcademicYear);

                                        MarkingPeriodTitle = ppData!.ShortName;
                                        startDate = ppData.StartDate;
                                        endDate = ppData.EndDate;
                                    }

                                    if (markingPeriodid.First() == "2")
                                    {
                                        QtrMarkingPeriodId = Int32.Parse(markingPeriodid.ElementAt(1));

                                        var qtrData = this.context?.Quarters.FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.MarkingPeriodId == QtrMarkingPeriodId && x.AcademicYear == reportCardViewModel.AcademicYear);

                                        MarkingPeriodTitle = qtrData!.ShortName;
                                        startDate = qtrData.StartDate;
                                        endDate = qtrData.EndDate;
                                    }

                                    if (markingPeriodid.First() == "1")
                                    {
                                        SmstrMarkingPeriodId = Int32.Parse(markingPeriodid.ElementAt(1));

                                        var smstrData = this.context?.Semesters.FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.MarkingPeriodId == SmstrMarkingPeriodId && x.AcademicYear == reportCardViewModel.AcademicYear);

                                        MarkingPeriodTitle = smstrData!.ShortName;
                                        startDate = smstrData.StartDate;
                                        endDate = smstrData.EndDate;
                                    }

                                    if (markingPeriodid.First() == "0")
                                    {
                                        YrMarkingPeriodId = Int32.Parse(markingPeriodid.ElementAt(1));

                                        var yrData = this.context?.SchoolYears.FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.MarkingPeriodId == YrMarkingPeriodId && x.AcademicYear == reportCardViewModel.AcademicYear);

                                        MarkingPeriodTitle = yrData!.ShortName;
                                        startDate = yrData.StartDate;
                                        endDate = yrData.EndDate;
                                    }                                   

                                    var reportCardData = this.context?.StudentFinalGrade.Include(x => x.StudentFinalGradeComments).Include(x => x.StudentFinalGradeStandard).Where(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.StudentId == student.StudentId && x.AcademicYear == reportCardViewModel.AcademicYear && ((x.YrMarkingPeriodId != null && x.YrMarkingPeriodId == YrMarkingPeriodId) || (x.SmstrMarkingPeriodId != null && x.SmstrMarkingPeriodId == SmstrMarkingPeriodId) || (x.QtrMarkingPeriodId != null && x.QtrMarkingPeriodId == QtrMarkingPeriodId) || (x.PrgrsprdMarkingPeriodId != null && x.PrgrsprdMarkingPeriodId == PrgrsprdMarkingPeriodId))).ToList();

                                    decimal? gPaValue = 0.0m;
                                    decimal? CreditEarned = 0.0m;
                                    decimal? CreditHours = 0.0m;

                                    if (reportCardData != null && reportCardData.Any())
                                    {
                                        foreach (var reportCard in reportCardData)
                                        {
                                            var CourseSectionData = this.context?.CourseSection.FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.CourseSectionId == reportCard.CourseSectionId && x.CourseId == reportCard.CourseId);

                                            var gradeData = this.context?.Grade.FirstOrDefault(x => x.TenantId == reportCard.TenantId && x.SchoolId == reportCard.SchoolId && x.Title == reportCard.GradeObtained && x.GradeScaleId == reportCard.GradeScaleId);

                                            if (gradeData != null)
                                            {
                                                //CreditHours = CourseSectionData.CreditHours;
                                                CreditHours = CourseSectionData!.CreditHours;
                                                CreditEarned = reportCard.CreditEarned != null ? reportCard.CreditEarned : CourseSectionData.CreditHours;
                                                gPaValue = CourseSectionData.IsWeightedCourse != true ? gradeData.UnweightedGpValue * (CreditHours / CreditEarned) : gradeData.WeightedGpValue * (CreditHours / CreditEarned);

                                            }
                                            var studentReportCardDetail = new StudentReportCardDetail()
                                            {
                                                Id = (long)ide,
                                                TenantId = reportCardViewModel.TenantId,
                                                SchoolId = reportCardViewModel.SchoolId,
                                                StudentId = student.StudentId,
                                                SchoolYear = reportCardViewModel.AcademicYear.ToString()!,
                                                GradeTitle = GradeLevelTitle!,
                                                MarkingPeriodTitle = MarkingPeriodTitle,
                                                CourseName = CourseSectionData!.CourseSectionName,
                                                Grade = reportCardViewModel.Parcentage != true ? reportCard.GradeObtained : reportCard.GradeObtained + "(" + reportCard.PercentMarks + ")",
                                                Gpa = reportCardViewModel.GPA == true ? gPaValue : null,
                                                CreatedBy = reportCardViewModel.CreatedBy,
                                                CreatedOn = DateTime.UtcNow
                                            };
                                            studentReportCardDetailList.Add(studentReportCardDetail);
                                            ide++;
                                        }
                                        this.context?.StudentReportCardDetail.AddRange(studentReportCardDetailList);
                                    }

                                    var attendanceData = this.context?.AttendanceCodeCategories.Include(x => x.AttendanceCode).FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.AcademicYear == reportCardViewModel.AcademicYear);

                                    if(attendanceData!=null)
                                    {
                                        foreach (var Attendance in attendanceData.AttendanceCode.ToList())
                                        {
                                            var studentDailyAttendanceCount = this.context?.StudentDailyAttendance.Where(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.StudentId == student.StudentId && x.AttendanceCode == Attendance.Title && x.AttendanceDate >= startDate && x.AttendanceDate <= endDate).ToList().Count;
                                          
                                        }
                                    }

                                    var studentReportCardMaster = new StudentReportCardMaster
                                    {
                                        TenantId = reportCardViewModel.TenantId,
                                        SchoolId = reportCardViewModel.SchoolId,
                                        StudentId = student.StudentId,
                                        SchoolYear = reportCardViewModel.AcademicYear.ToString()!,
                                        GradeTitle = GradeLevelTitle!,
                                        StudentInternalId = studentData.StudentInternalId,
                                        ReportGenerationDate = DateTime.UtcNow,
                                        MarkingPeriodTitle = MarkingPeriodTitle!,
                                        CreatedBy = reportCardViewModel.CreatedBy,
                                        CreatedOn = DateTime.UtcNow
                                    };
                                    studentReportCardMasterList.Add(studentReportCardMaster);
                                }
                            }
                            this.context?.StudentReportCardMaster.AddRange(studentReportCardMasterList);
                            i++;
                        }
                    }
                    this.context?.SaveChanges();
                    reportCardViewModel._message = "Added Successfully";
                }
                else
                {
                    reportCardViewModel._failure = true;
                    reportCardViewModel._message = "Select Student Please";
                }
            }
            catch (Exception es)
            {
                reportCardViewModel._failure = true;
                reportCardViewModel._message = es.Message;
            }
            return reportCardViewModel;
        }


        /// <summary>
        /// Generate Report Card
        /// </summary>
        /// <param name="reportCardViewModel"></param>
        /// <returns></returns>
        public async Task<ReportCardViewModel> GenerateReportCard(ReportCardViewModel reportCardViewModel)
        {
            ReportCardViewModel reportCardView = new ReportCardViewModel();
            try
            {
                reportCardView.TenantId = reportCardViewModel.TenantId;
                reportCardView.SchoolId = reportCardViewModel.SchoolId;
                reportCardView._tenantName = reportCardViewModel._tenantName;
                reportCardView._userName = reportCardViewModel._userName;
                string? base64 = null;
                object data = new object();

                List<object> reportCardList = new List<object>();
                List<object> teacherCommentList = new List<object>();

                if (reportCardViewModel?.TemplateType?.ToLower() == "default")
                {

                    foreach (var student in reportCardViewModel.studentsReportCardViewModelList)
                    {
                        var studentNameData = this.context?.StudentMaster.FirstOrDefault(x => x.SchoolId == reportCardViewModel.SchoolId && x.TenantId == reportCardViewModel.TenantId && x.StudentId == student.StudentId);

                        var schoolData = this.context?.SchoolMaster.FirstOrDefault(x => x.SchoolId == reportCardViewModel.SchoolId && x.TenantId == reportCardViewModel.TenantId);

                        var studentReportCardData = this.context?.StudentReportCardMaster.Include(x => x.StudentReportCardDetail).Where(x => x.SchoolId == reportCardViewModel.SchoolId && x.TenantId == reportCardViewModel.TenantId && x.SchoolYear == reportCardViewModel.AcademicYear.ToString() && x.StudentId == student.StudentId).ToList();

                        if (studentNameData != null && schoolData != null && studentReportCardData != null && studentReportCardData.Any())
                        {
                            List<object> reportDetailsList = new List<object>();
                            foreach (var studentReportCard in studentReportCardData)
                            {
                                var studentReportCardDetailsData = studentReportCard.StudentReportCardDetail.Where(x => x.MarkingPeriodTitle == studentReportCard.MarkingPeriodTitle).ToList();

                                //var teacherCommentsData = studentReportCardDetailsData.Where(x => x.OverallTeacherComments != null && x.TeacherComments != null).ToList().Select(x => new { x.TeacherComments, x.OverallTeacherComments });

                                var teacherCommentsData = studentReportCardDetailsData.Where(x => x.OverallTeacherComments != null && x.TeacherComments != null).ToList().Select(x => new { x?.TeacherComments, x?.OverallTeacherComments });

                                teacherCommentList.AddRange(teacherCommentsData);
                                object markingPeriodWiseData = new
                                {
                                    studentReportCard.MarkingPeriodTitle,
                                    studentReportCard.ExcusedAbsences,
                                    studentReportCard.Absences,
                                    Details = studentReportCardDetailsData,

                                };
                                reportDetailsList.Add(markingPeriodWiseData);
                            }

                            object reportCard = new
                            {
                                SchoolData = schoolData,
                                studentInternalId = studentReportCardData.FirstOrDefault()!.StudentInternalId,
                                gradeTitle = studentReportCardData.FirstOrDefault()!.GradeTitle,
                                yodAttendance = studentReportCardData.FirstOrDefault()!.YodAttendance,
                                yodAbsence = studentReportCardData.FirstOrDefault()!.YodAbsence,
                                ReportdetailsData = reportDetailsList,
                                StudentName = studentNameData,

                            };
                            reportCardList.Add(reportCard);
                        }
                    }

                    if (reportCardList != null)
                    {
                        var courseCommentCategoryData = this.context?.CourseCommentCategory.Where(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId).ToList();

                        data = new
                        {
                            TotalData = reportCardList,
                            CourseCommentCategoryData = courseCommentCategoryData,
                            TeacherCommentList = reportCardViewModel.TeacherComments == true ? teacherCommentList : null
                        };
                    }

                    GenerateReportCard _report = new GenerateReportCard();
                    var message = await _report.Generate(data);

                    bool isWindows = System.Runtime.InteropServices.RuntimeInformation
                                           .IsOSPlatform(OSPlatform.Windows);
                    if (isWindows)
                    {
                        using (var fileStream = new FileStream(@"ReportCard\\StudentReport.pdf", FileMode.Open))
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                fileStream.CopyTo(memoryStream);
                                byte[] bytes = memoryStream.ToArray();
                                base64 = Convert.ToBase64String(bytes);
                                fileStream.Close();
                            }
                        }
                        reportCardView.ReportCardPdf = base64;
                    }
                    else
                    {
                        using (var fileStream = new FileStream(@"ReportCard/StudentReport.pdf", FileMode.Open))
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                fileStream.CopyTo(memoryStream);
                                byte[] bytes = memoryStream.ToArray();
                                base64 = Convert.ToBase64String(bytes);
                                fileStream.Close();
                            }
                        }
                        reportCardView.ReportCardPdf = base64;
                    }
                }
                else
                {
                    if (reportCardViewModel!.studentsReportCardViewModelList is null)
                    {
                        return reportCardViewModel;
                    }
                    SchoolMaster schoolData = new SchoolMaster();
                    foreach (var student in reportCardViewModel.studentsReportCardViewModelList)
                    {
                        var studentData = this.context?.StudentMaster.Include(s => s.Sections).FirstOrDefault(x => x.SchoolId == reportCardViewModel.SchoolId && x.TenantId == reportCardViewModel.TenantId && x.StudentId == student.StudentId);

                        //schoolData = this.context?.SchoolMaster.Include(x => x.SchoolDetail).Include(x => x.GradeScale).ThenInclude(x => x.Grade).Include(s => s.AttendanceCodeCategories).ThenInclude(s => s.AttendanceCode).FirstOrDefault(x => x.SchoolId == reportCardViewModel.SchoolId && x.TenantId == reportCardViewModel.TenantId);

                        schoolData = this.context?.SchoolMaster.Include(x => x.SchoolDetail).Include(x => x.GradeScale).ThenInclude(x => x.Grade).Include(s => s.AttendanceCodeCategories).ThenInclude(s => s.AttendanceCode).FirstOrDefault(x => x.SchoolId == reportCardViewModel.SchoolId && x.TenantId == reportCardViewModel.TenantId)!;

                        var studentReportCardData = this.context?.StudentReportCardMaster.Include(x => x.StudentReportCardDetail).Where(x => x.SchoolId == reportCardViewModel.SchoolId && x.TenantId == reportCardViewModel.TenantId && x.SchoolYear == reportCardViewModel.AcademicYear.ToString() && x.StudentId == student.StudentId).ToList();

                        if (studentData != null && schoolData != null && studentReportCardData?.Any() == true)
                        {
                            List<object> reportDetailsList = new List<object>();
                            List<object> attendanceList = new List<object>();
                            int? count = studentReportCardData?.Count;
                            List<string> courseSectionList = new List<string>();
                            List<object> courseSectionWithGradeList = new List<object>();

                            foreach (var studentReportCard in studentReportCardData!)
                            {
                                var courseSection = studentReportCard.StudentReportCardDetail.Where(x => x.TenantId == reportCardViewModel.TenantId && x.StudentId == student.StudentId).Select(s => s.CourseName).ToList();
                                if(courseSection?.Any()==true)
                                {
                                    courseSectionList.AddRange(courseSection!);
                                    courseSectionList = courseSectionList.Distinct().ToList();
                                }
                            }

                            var reportCardDetailsList = this.context?.StudentReportCardDetail.Where(x => x.SchoolId == reportCardViewModel.SchoolId && x.TenantId == reportCardViewModel.TenantId && x.SchoolYear == reportCardViewModel.AcademicYear.ToString() && x.StudentId == student.StudentId).ToList();
                            foreach (var cs in courseSectionList)
                            {
                                List<object> csGradeMPWiseList = new List<object>();

                                foreach (var studentReportCard in studentReportCardData)
                                {
                                    var csGradeInMP = reportCardDetailsList!.FirstOrDefault(x => x.MarkingPeriodTitle == studentReportCard.MarkingPeriodTitle && x.CourseName==cs);

                                    if (csGradeInMP != null)
                                    {
                                        object csGradeMPWise = new
                                        {
                                            grade = csGradeInMP.Grade
                                        };
                                        csGradeMPWiseList.Add(csGradeMPWise);
                                    }
                                    else
                                    {
                                        object csGradeMPWise = new
                                        {
                                            grade = "N/A"
                                        };
                                        csGradeMPWiseList.Add(csGradeMPWise);
                                    }
                                }
                                object csDetails = new
                                {
                                    courseSectionTitle= cs,
                                    gradeList = csGradeMPWiseList
                                };
                                courseSectionWithGradeList.Add(csDetails);
                            }

                            //this block for GPA
                            List<object> gpaList = new List<object>();
                            foreach (var studentReportCard in studentReportCardData)
                            {
                                var gpa = studentReportCard.StudentReportCardDetail.Sum(x => x.Gpa);
                                var csCount = studentReportCard.StudentReportCardDetail.Count;
                                decimal? Gpavalue = 0.0m;
                                if (gpa > 0 && csCount > 0)
                                {
                                    Gpavalue = Math.Round((decimal)(gpa / csCount), 2);
                                }

                                object GPA = new
                                {
                                    gpaValue = Gpavalue
                                };
                                gpaList.Add(GPA);
                            }

                            foreach (var studentReportCard in studentReportCardData)
                            {
                                //var studentReportCardDetailsData = studentReportCard.StudentReportCardDetail.Where(x => x.MarkingPeriodTitle == studentReportCard.MarkingPeriodTitle).ToList();

                                object markingPeriodWiseData = new
                                {
                                    MarkingPeriod = studentReportCard.MarkingPeriodTitle,
                                    //Details = studentReportCardDetailsData,
                                };
                                reportDetailsList.Add(markingPeriodWiseData);                               
                            }
                           
                            //this block for attendance
                            if (schoolData.AttendanceCodeCategories.Count > 0)
                            {
                                foreach (var AttendanceCode in schoolData.AttendanceCodeCategories.FirstOrDefault()!.AttendanceCode)
                                {
                                    List<object> AttendanceCountList = new List<object>();
                                    var markingPeriodsData = reportCardViewModel.MarkingPeriods!.Split(",");
                                    DateTime? startDate = null;
                                    DateTime? endDate = null;
                                    string? MarkingPeriodTitle = null;

                                    foreach (var markingPeriod in markingPeriodsData)
                                    {
                                        int? QtrMarkingPeriodId = null;
                                        int? SmstrMarkingPeriodId = null;
                                        int? YrMarkingPeriodId = null;
                                        int? PrgrsprdMarkingPeriodId = null;

                                        if (markingPeriod != null)
                                        {
                                            var markingPeriodid = markingPeriod.Split("_", StringSplitOptions.RemoveEmptyEntries);

                                            if (markingPeriodid.First() == "3")
                                            {
                                                PrgrsprdMarkingPeriodId = Int32.Parse(markingPeriodid.ElementAt(1));

                                                var ppData = this.context?.ProgressPeriods.FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.MarkingPeriodId == PrgrsprdMarkingPeriodId && x.AcademicYear == reportCardViewModel.AcademicYear);

                                                MarkingPeriodTitle = ppData!.ShortName;
                                                startDate = ppData.StartDate;
                                                endDate = ppData.EndDate;
                                            }

                                            if (markingPeriodid.First() == "2")
                                            {
                                                QtrMarkingPeriodId = Int32.Parse(markingPeriodid.ElementAt(1));

                                                var qtrData = this.context?.Quarters.FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.MarkingPeriodId == QtrMarkingPeriodId && x.AcademicYear == reportCardViewModel.AcademicYear);

                                                MarkingPeriodTitle = qtrData!.ShortName;
                                                startDate = qtrData.StartDate;
                                                endDate = qtrData.EndDate;
                                            }

                                            if (markingPeriodid.First() == "1")
                                            {
                                                SmstrMarkingPeriodId = Int32.Parse(markingPeriodid.ElementAt(1));

                                                var smstrData = this.context?.Semesters.FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.MarkingPeriodId == SmstrMarkingPeriodId && x.AcademicYear == reportCardViewModel.AcademicYear);

                                                MarkingPeriodTitle = smstrData!.ShortName;
                                                startDate = smstrData.StartDate;
                                                endDate = smstrData.EndDate;
                                            }

                                            if (markingPeriodid.First() == "0")
                                            {
                                                YrMarkingPeriodId = Int32.Parse(markingPeriodid.ElementAt(1));

                                                var yrData = this.context?.SchoolYears.FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.MarkingPeriodId == YrMarkingPeriodId && x.AcademicYear == reportCardViewModel.AcademicYear);

                                                MarkingPeriodTitle = yrData!.ShortName;
                                                startDate = yrData.StartDate;
                                                endDate = yrData.EndDate;
                                            }

                                            var studentDailyAttendanceCount = this.context?.StudentDailyAttendance.Where(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.StudentId == student.StudentId && x.AttendanceCode == AttendanceCode.Title && x.AttendanceDate >= startDate && x.AttendanceDate <= endDate).ToList().Count;
                                            object attendanceCount = new
                                            {
                                                AttendanceCount = studentDailyAttendanceCount
                                            };
                                            AttendanceCountList.Add(attendanceCount);
                                        }
                                    }
                                    object Attendance = new
                                    {
                                        AttendanceCodeTitle = AttendanceCode.Title,
                                        AttendanceCountList
                                    };
                                    attendanceList.Add(Attendance);
                                }
                            }

                            object reportCard = new
                            {
                                SchoolData = schoolData,
                                gradeTitle = studentReportCardData != null ? studentReportCardData.FirstOrDefault()!.GradeTitle : null,
                                ReportdetailsData = reportDetailsList,
                                StudentData = studentData,
                                AttendanceCode = attendanceList,
                                CourseSectionWithGradeList=courseSectionWithGradeList,
                                GPAList= gpaList
                            };
                            reportCardList.Add(reportCard);
                        }
                    }

                    if (reportCardList != null)
                    {
                        data = new
                        {
                            TotalData = reportCardList,
                            GradeData = schoolData.GradeScale.Count > 0 ? schoolData.GradeScale.FirstOrDefault()!.Grade.ToList() : null,
                        };
                    }

                    if (String.Compare(reportCardViewModel.TemplateType, "chuuk", true) == 0)
                    {
                        GenerateChuukReportCard _report = new GenerateChuukReportCard();
                        var message = await _report.Generate(data);

                        bool isWindows = System.Runtime.InteropServices.RuntimeInformation
                                               .IsOSPlatform(OSPlatform.Windows);
                        if (isWindows)
                        {
                            using (var fileStream = new FileStream(@"ReportCard\\StudentChuukReportCard.pdf", FileMode.Open))
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    fileStream.CopyTo(memoryStream);
                                    byte[] bytes = memoryStream.ToArray();
                                    base64 = Convert.ToBase64String(bytes);
                                    fileStream.Close();
                                }
                            }
                            reportCardView.ReportCardPdf = base64;
                        }
                        else
                        {
                            using (var fileStream = new FileStream(@"ReportCard/StudentChuukReportCard.pdf", FileMode.Open))
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    fileStream.CopyTo(memoryStream);
                                    byte[] bytes = memoryStream.ToArray();
                                    base64 = Convert.ToBase64String(bytes);
                                    fileStream.Close();
                                }
                            }
                            reportCardView.ReportCardPdf = base64;
                        }
                    }

                    if (String.Compare(reportCardViewModel.TemplateType, "kosrae", true) == 0)
                    {
                        GenerateKosraeReportCard _report = new GenerateKosraeReportCard();
                        var message = await _report.Generate(data);

                        //if (message == "success")
                        //{
                        //    using (var fileStream = new FileStream(@"ReportCard\\StudentKosraeReportCard.pdf", FileMode.Open))
                        //    {
                        //        using (var memoryStream = new MemoryStream())
                        //        {
                        //            fileStream.CopyTo(memoryStream);
                        //            byte[] bytes = memoryStream.ToArray();
                        //            base64 = Convert.ToBase64String(bytes);
                        //            fileStream.Close();
                        //        }
                        //    }
                        //    reportCardView.ReportCardPdf = base64;
                        //}
                        //else
                        //{
                        //    reportCardView._message = "Problem occur!!! Please Try Again";
                        //    reportCardView._failure = true;
                        //}

                        bool isWindows = System.Runtime.InteropServices.RuntimeInformation
                                              .IsOSPlatform(OSPlatform.Windows);
                        if (isWindows)
                        {
                            using (var fileStream = new FileStream(@"ReportCard\\StudentKosraeReportCard.pdf", FileMode.Open))
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    fileStream.CopyTo(memoryStream);
                                    byte[] bytes = memoryStream.ToArray();
                                    base64 = Convert.ToBase64String(bytes);
                                    fileStream.Close();
                                }
                            }
                            reportCardView.ReportCardPdf = base64;
                        }
                        else
                        {
                            using (var fileStream = new FileStream(@"ReportCard/StudentKosraeReportCard.pdf", FileMode.Open))
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    fileStream.CopyTo(memoryStream);
                                    byte[] bytes = memoryStream.ToArray();
                                    base64 = Convert.ToBase64String(bytes);
                                    fileStream.Close();
                                }
                            }
                            reportCardView.ReportCardPdf = base64;
                        }
                    }

                    if (String.Compare(reportCardViewModel.TemplateType, "pohnpei", true) == 0)
                    {
                        GeneratePohnpeiReportCard _report = new GeneratePohnpeiReportCard();
                        var message = await _report.Generate(data);

                        bool isWindows = System.Runtime.InteropServices.RuntimeInformation
                                             .IsOSPlatform(OSPlatform.Windows);
                        if (isWindows)
                        {
                            using (var fileStream = new FileStream(@"ReportCard\\StudentPohnpeiReportCard.pdf", FileMode.Open))
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    fileStream.CopyTo(memoryStream);
                                    byte[] bytes = memoryStream.ToArray();
                                    base64 = Convert.ToBase64String(bytes);
                                    fileStream.Close();
                                }
                            }
                            reportCardView.ReportCardPdf = base64;
                        }
                        else
                        {
                            using (var fileStream = new FileStream(@"ReportCard/StudentPohnpeiReportCard.pdf", FileMode.Open))
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    fileStream.CopyTo(memoryStream);
                                    byte[] bytes = memoryStream.ToArray();
                                    base64 = Convert.ToBase64String(bytes);
                                    fileStream.Close();
                                }
                            }
                            reportCardView.ReportCardPdf = base64;
                        }
                    }

                    if (String.Compare(reportCardViewModel.TemplateType, "yap", true) == 0)
                    {
                        GenerateYapReportCard _report = new GenerateYapReportCard();
                        var message = await _report.Generate(data);

                        bool isWindows = System.Runtime.InteropServices.RuntimeInformation
                                            .IsOSPlatform(OSPlatform.Windows);
                        if (isWindows)
                        {
                            using (var fileStream = new FileStream(@"ReportCard\\StudentYapReportCard.pdf", FileMode.Open))
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    fileStream.CopyTo(memoryStream);
                                    byte[] bytes = memoryStream.ToArray();
                                    base64 = Convert.ToBase64String(bytes);
                                    fileStream.Close();
                                }
                            }
                            reportCardView.ReportCardPdf = base64;
                        }
                        else
                        {
                            using (var fileStream = new FileStream(@"ReportCard/StudentYapReportCard.pdf", FileMode.Open))
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    fileStream.CopyTo(memoryStream);
                                    byte[] bytes = memoryStream.ToArray();
                                    base64 = Convert.ToBase64String(bytes);
                                    fileStream.Close();
                                }
                            }
                            reportCardView.ReportCardPdf = base64;

                        }
                    }
                }
            }
            catch (Exception es)
            {
                reportCardView._message = es.Message;
                reportCardView._failure = true;
            }
            return reportCardView;
        }

        /// <summary>
        /// Get Report Card For Students
        /// </summary>
        /// <param name="reportCardViewModel"></param>
        /// <returns></returns>
        public ReportCardViewModel GetReportCardForStudents(ReportCardViewModel reportCardViewModel)
        {
            ReportCardViewModel reportCardView = new ReportCardViewModel();
            reportCardView._tenantName = reportCardViewModel._tenantName;
            reportCardView._token = reportCardViewModel._token;
            reportCardView.TenantId = reportCardViewModel.TenantId;
            reportCardView.SchoolId = reportCardViewModel.SchoolId;
            reportCardView.AcademicYear = reportCardViewModel.AcademicYear;
            reportCardView.TemplateType = reportCardViewModel.TemplateType;
            try
            {
                if (reportCardViewModel.studentsReportCardViewModelList.Count > 0)
                {
                    string? schoolYear = null;

                    var schoolData = this.context?.SchoolMaster.Include(x => x.SchoolDetail).Include(x => x.SchoolYears).Include(x => x.GradeScale).ThenInclude(x => x.Grade).FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId);

                    if (schoolData != null)
                    {
                        var schoolYearData = schoolData.SchoolYears.FirstOrDefault(x => x.AcademicYear == reportCardViewModel.AcademicYear);
                        if (schoolYearData != null)
                        {
                            schoolYear = schoolYearData.StartDate!.Value.Year + "-" + schoolYearData.EndDate!.Value.Year;
                        }
                    }

                    var studentMasterData = this.context?.StudentMaster.Include(x => x.StudentEnrollment).Where(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId).ToList();

                    var studentAttendanceMasterData = this.context?.StudentAttendance.Include(x => x.AttendanceCodeNavigation).Where(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId).ToList();

                    if (String.Compare(reportCardViewModel.TemplateType, "default", true) == 0)
                    {
                        var courseCommentCategoryData = this.context?.CourseCommentCategory.Where(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId).Select(s => new CourseCommentCategory { TenantId = s.TenantId, SchoolId = s.SchoolId, CourseCommentId = s.CourseCommentId, Comments = s.Comments }).ToList();

                        foreach (var student in reportCardViewModel.studentsReportCardViewModelList)
                        {
                            StudentsReportCardViewModel studentsReportCard = new StudentsReportCardViewModel();
                            List<string> teacherComments = new List<string>();
                            int teacherCommentsNo = 1;
                            int? absencesInDays = 0;

                            var studentData = studentMasterData!.FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.StudentId == student.StudentId);

                            var GradeLevelTitle = studentData!.StudentEnrollment.Where(x => x.IsActive == true).Select(s => s.GradeLevelTitle).FirstOrDefault();

                            studentsReportCard.SchoolName = schoolData!.SchoolName;
                            studentsReportCard.SchoolYear = schoolYear;
                            studentsReportCard.FirstGivenName = studentData.FirstGivenName;
                            studentsReportCard.MiddleName = studentData.MiddleName;
                            studentsReportCard.LastFamilyName = studentData.LastFamilyName;
                            studentsReportCard.StudentInternalId = studentData.StudentInternalId;
                            studentsReportCard.GradeTitle = GradeLevelTitle;

                            var markingPeriodsData = reportCardViewModel.MarkingPeriods!.Split(",");
                            DateTime? startDate = null;
                            DateTime? endDate = null;
                            string? MarkingPeriodTitle = null;

                            foreach (var markingPeriod in markingPeriodsData)
                            {
                                MarkingPeriodDetailsForDefaultTemplate markingPeriodDetailsForDefaultTemplates = new MarkingPeriodDetailsForDefaultTemplate();
                                List<DateTime> holidayList = new List<DateTime>();
                                int? Absences = 0;
                                int? ExcusedAbsences = 0;
                                int? QtrMarkingPeriodId = null;
                                int? SmstrMarkingPeriodId = null;
                                int? YrMarkingPeriodId = null;
                                int? PrgrsprdMarkingPeriodId = null;
                                bool? Exam = null;

                                if (markingPeriod != null)
                                {
                                    var markingPeriodid = markingPeriod.Split("_", StringSplitOptions.RemoveEmptyEntries);

                                    if (markingPeriodid.First() == "3")
                                    {
                                        PrgrsprdMarkingPeriodId = Int32.Parse(markingPeriodid.ElementAt(1));

                                        var ppData = this.context?.ProgressPeriods.FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.MarkingPeriodId == PrgrsprdMarkingPeriodId && x.AcademicYear == reportCardViewModel.AcademicYear);

                                        MarkingPeriodTitle = ppData!.Title;
                                        startDate = ppData.StartDate;
                                        endDate = ppData.EndDate;

                                        if (markingPeriodid.Last() == "E")
                                        {
                                            Exam = true;
                                            MarkingPeriodTitle = ppData!.Title + " Exam";
                                        }
                                    }

                                    if (markingPeriodid.First() == "2")
                                    {
                                        QtrMarkingPeriodId = Int32.Parse(markingPeriodid.ElementAt(1));

                                        var qtrData = this.context?.Quarters.FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.MarkingPeriodId == QtrMarkingPeriodId && x.AcademicYear == reportCardViewModel.AcademicYear);

                                        MarkingPeriodTitle = qtrData!.Title;
                                        startDate = qtrData.StartDate;
                                        endDate = qtrData.EndDate;

                                        if (markingPeriodid.Last() == "E")
                                        {
                                            Exam = true;
                                            MarkingPeriodTitle = qtrData!.Title + " Exam";
                                        }
                                    }

                                    if (markingPeriodid.First() == "1")
                                    {
                                        SmstrMarkingPeriodId = Int32.Parse(markingPeriodid.ElementAt(1));

                                        var smstrData = this.context?.Semesters.FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.MarkingPeriodId == SmstrMarkingPeriodId && x.AcademicYear == reportCardViewModel.AcademicYear);

                                        MarkingPeriodTitle = smstrData!.Title;
                                        startDate = smstrData.StartDate;
                                        endDate = smstrData.EndDate;

                                        if (markingPeriodid.Last() == "E")
                                        {
                                            Exam = true;
                                            MarkingPeriodTitle = smstrData!.Title + " Exam";
                                        }
                                    }

                                    if (markingPeriodid.First() == "0")
                                    {
                                        YrMarkingPeriodId = Int32.Parse(markingPeriodid.ElementAt(1));

                                        var yrData = this.context?.SchoolYears.FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.MarkingPeriodId == YrMarkingPeriodId && x.AcademicYear == reportCardViewModel.AcademicYear);

                                        MarkingPeriodTitle = yrData!.Title;
                                        startDate = yrData.StartDate;
                                        endDate = yrData.EndDate;

                                        if (markingPeriodid.Last() == "E")
                                        {
                                            Exam = true;
                                            MarkingPeriodTitle = yrData!.Title + " Exam";
                                        }
                                    }
                                    markingPeriodDetailsForDefaultTemplates.MarkingPeriodTitle = MarkingPeriodTitle;

                                    var studentAttendanceData = studentAttendanceMasterData!.Where(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.StudentId == student.StudentId && x.AttendanceDate >= startDate && x.AttendanceDate <= endDate).ToList();

                                    if (studentAttendanceData.Count > 0)
                                    {
                                        Absences = studentAttendanceData.Where(x => x.AttendanceCodeNavigation.StateCode.ToLower() == "absent").Count();
                                        ExcusedAbsences = studentAttendanceData.Where(x => x.AttendanceCodeNavigation.StateCode.ToLower() == "excusedabsent").Count();
                                        absencesInDays += Absences + ExcusedAbsences;
                                    }
                                    var reportCardData = new List<StudentFinalGrade>();

                                    if (Exam == true)
                                    {
                                        reportCardData = this.context?.StudentFinalGrade.Include(x => x.StudentFinalGradeComments).Include(x => x.StudentFinalGradeStandard).Where(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.StudentId == student.StudentId && x.AcademicYear == reportCardViewModel.AcademicYear && ((x.YrMarkingPeriodId != null && x.YrMarkingPeriodId == YrMarkingPeriodId) || (x.SmstrMarkingPeriodId != null && x.SmstrMarkingPeriodId == SmstrMarkingPeriodId) || (x.QtrMarkingPeriodId != null && x.QtrMarkingPeriodId == QtrMarkingPeriodId) || (x.PrgrsprdMarkingPeriodId != null && x.PrgrsprdMarkingPeriodId == PrgrsprdMarkingPeriodId)) && x.IsExamGrade == true).ToList();
                                    }
                                    else
                                    {
                                        reportCardData = this.context?.StudentFinalGrade.Include(x => x.StudentFinalGradeComments).Include(x => x.StudentFinalGradeStandard).Where(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.StudentId == student.StudentId && x.AcademicYear == reportCardViewModel.AcademicYear && ((x.YrMarkingPeriodId != null && x.YrMarkingPeriodId == YrMarkingPeriodId) || (x.SmstrMarkingPeriodId != null && x.SmstrMarkingPeriodId == SmstrMarkingPeriodId) || (x.QtrMarkingPeriodId != null && x.QtrMarkingPeriodId == QtrMarkingPeriodId) || (x.PrgrsprdMarkingPeriodId != null && x.PrgrsprdMarkingPeriodId == PrgrsprdMarkingPeriodId)) && x.IsExamGrade != true).ToList();
                                    }

                                    decimal? gPaValue = 0.0m;
                                    decimal? CreditEarned = 0.0m;
                                    decimal? CreditHours = 0.0m;

                                    if (reportCardData?.Any() == true)
                                    {
                                        foreach (var reportCard in reportCardData)
                                        {
                                            CourseSectionGradeDetailsForDefaultTemplate courseSectionGradeDetailsForDefaultTemplate = new CourseSectionGradeDetailsForDefaultTemplate();

                                            var CourseSectionData = this.context?.CourseSection.Include(x => x.GradeScale).Include(x => x.StaffCoursesectionSchedule).ThenInclude(x => x.StaffMaster).FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.CourseSectionId == reportCard.CourseSectionId && x.CourseId == reportCard.CourseId);

                                            //var gradeData = CourseSectionData.GradeScale.Grade.FirstOrDefault(x => x.TenantId == reportCard.TenantId && x.SchoolId == reportCard.SchoolId && x.Title.ToLower() == reportCard.GradeObtained.ToLower() && x.GradeScaleId == reportCard.GradeScaleId);

                                            var gradeData = CourseSectionData?.GradeScale?.Grade.AsEnumerable().Where(x => x.TenantId == reportCard.TenantId && x.SchoolId == reportCard.SchoolId && String.Compare(x.Title, reportCard.GradeObtained, true) == 0 && x.GradeScaleId == reportCard.GradeScaleId).FirstOrDefault();

                                            if (gradeData != null)
                                            {
                                                CreditHours = CourseSectionData?.CreditHours;
                                                CreditEarned = reportCard.CreditEarned != null ? reportCard.CreditEarned : CourseSectionData?.CreditHours;
                                                gPaValue = CourseSectionData?.IsWeightedCourse != true ? gradeData.UnweightedGpValue * (CreditHours / CreditEarned) : gradeData.WeightedGpValue * (CreditHours / CreditEarned);
                                            }

                                            var comments = reportCard.StudentFinalGradeComments.Select(x => x.CourseCommentId).ToList();
                                            var Comments = string.Join(",", comments.Select(x => x.ToString()).ToArray());

                                            courseSectionGradeDetailsForDefaultTemplate.CourseSectionName = CourseSectionData?.CourseSectionName;
                                            courseSectionGradeDetailsForDefaultTemplate.StaffName = CourseSectionData?.StaffCoursesectionSchedule.Count > 0 ? CourseSectionData?.StaffCoursesectionSchedule.FirstOrDefault()!.StaffMaster.FirstGivenName + " " + CourseSectionData?.StaffCoursesectionSchedule.FirstOrDefault()!.StaffMaster.MiddleName + " " + CourseSectionData?.StaffCoursesectionSchedule.FirstOrDefault()!.StaffMaster.LastFamilyName : null;
                                            courseSectionGradeDetailsForDefaultTemplate.GradeObtained = reportCard.GradeObtained;
                                            courseSectionGradeDetailsForDefaultTemplate.PercentMarks = reportCard.PercentMarks;
                                            courseSectionGradeDetailsForDefaultTemplate.GPA = gPaValue;
                                            courseSectionGradeDetailsForDefaultTemplate.Comments = Comments;

                                            if (reportCard.TeacherComment != null)
                                            {
                                                courseSectionGradeDetailsForDefaultTemplate.TeacherComments = teacherCommentsNo++.ToString();
                                                teacherComments.Add(reportCard.TeacherComment);
                                            }
                                            markingPeriodDetailsForDefaultTemplates.courseSectionGradeDetailsForDefaultTemplates.Add(courseSectionGradeDetailsForDefaultTemplate);
                                        }
                                    }

                                    int workDays = 0;
                                    double attendencePercent = 0;

                                    var calenderData = this.context?.SchoolCalendars.FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.DefaultCalender == true && x.AcademicYear == reportCardViewModel.AcademicYear);

                                    var schoolYearData = schoolData.SchoolYears.FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.AcademicYear == reportCardViewModel.AcademicYear);

                                    if (calenderData != null && schoolYearData != null)
                                    {
                                        DateTime? schoolYearStartDate = schoolYearData.StartDate;
                                        DateTime? schoolYearEndDate = schoolYearData.EndDate;

                                        // Calculate Holiday
                                        var CalendarEventsData = this.context?.CalendarEvents.Where(e => e.TenantId == reportCardViewModel.TenantId && e.AcademicYear == reportCardViewModel.AcademicYear && (e.StartDate >= schoolYearStartDate && e.StartDate <= schoolYearEndDate || e.EndDate >= schoolYearStartDate && e.EndDate <= schoolYearEndDate) && e.IsHoliday == true && (e.SchoolId == reportCardViewModel.SchoolId || e.ApplicableToAllSchool == true)).ToList();

                                        if (CalendarEventsData?.Any() == true)
                                        {
                                            foreach (var calender in CalendarEventsData)
                                            {
                                                if (calender.EndDate!.Value.Date > calender.StartDate!.Value.Date)
                                                {
                                                    var date = Enumerable.Range(0, 1 + (calender.EndDate.Value.Date - calender.StartDate.Value.Date).Days)
                                                       .Select(i => calender.StartDate.Value.Date.AddDays(i))
                                                       .ToList();
                                                    holidayList.AddRange(date);
                                                }
                                                holidayList.Add(calender.StartDate.Value.Date);
                                            }
                                        }

                                        var daysValue = "0123456";
                                        var weekdays = calenderData.Days;
                                        var WeekOffDays = Regex.Split(daysValue, weekdays!);
                                        var WeekOfflist = new List<string>();

                                        foreach (var WeekOffDay in WeekOffDays)
                                        {
                                            Days days = new Days();
                                            var Day = Enum.GetName(days.GetType(), Convert.ToInt32(WeekOffDay));
                                            WeekOfflist.Add(Day!);
                                        }

                                        List<DateTime> allDates = new List<DateTime>();
                                        for (DateTime date = (DateTime)schoolYearStartDate!; date <= schoolYearEndDate; date = date.AddDays(1))
                                            allDates.Add(date);

                                        //remove holidays & weekofdays
                                        foreach (var date in allDates)
                                        {
                                            var Day = date.DayOfWeek.ToString();
                                            if (!WeekOfflist.Contains(Day) && !holidayList.Contains(date))
                                            {
                                                workDays++;
                                            }
                                        }

                                        var studentDailyAttendanceCount = this.context?.StudentDailyAttendance.Where(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.StudentId == student.StudentId && x.AttendanceCode.ToLower() == "present" && x.AttendanceDate >= startDate && x.AttendanceDate <= endDate).ToList().Count;

                                        if (studentDailyAttendanceCount > 0)
                                        {
                                            attendencePercent = (double)((studentDailyAttendanceCount / workDays) * 100);
                                        }
                                    }

                                    markingPeriodDetailsForDefaultTemplates.Absences = Absences;
                                    markingPeriodDetailsForDefaultTemplates.ExcusedAbsences = ExcusedAbsences;
                                    studentsReportCard.YearToDateAttendencePercent = Math.Round(attendencePercent, 2).ToString() + "%";
                                    studentsReportCard.YearToDateAbsencesInDays = absencesInDays;
                                    studentsReportCard.teacherCommentList = teacherComments;
                                    studentsReportCard.courseCommentCategories = courseCommentCategoryData!;
                                    studentsReportCard.markingPeriodDetailsForDefaultTemplates.Add(markingPeriodDetailsForDefaultTemplates);

                                }
                            }
                            reportCardView.studentsReportCardViewModelList.Add(studentsReportCard);
                        }
                    }
                    else
                    {
                        foreach (var student in reportCardViewModel.studentsReportCardViewModelList)
                        {
                            StudentsReportCardViewModel studentsReportCardViewModel = new StudentsReportCardViewModel();

                            var studentData = this.context?.StudentMaster.Include(x => x.Sections).Include(x => x.StudentEnrollment).FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.StudentId == student.StudentId);

                            var GradeLevelTitle = studentData!.StudentEnrollment.Where(x => x.IsActive == true).Select(s => s.GradeLevelTitle).FirstOrDefault();

                            studentsReportCardViewModel.SchoolName = schoolData!.SchoolName;
                            studentsReportCardViewModel.SchoolLogo = schoolData!.SchoolDetail.FirstOrDefault()!.SchoolLogo;
                            studentsReportCardViewModel.SchoolYear = schoolYear;
                            studentsReportCardViewModel.StudentId = studentData.StudentId; studentsReportCardViewModel.StudentInternalId = studentData.StudentInternalId;
                            studentsReportCardViewModel.FirstGivenName = studentData.FirstGivenName;
                            studentsReportCardViewModel.MiddleName = studentData.MiddleName;
                            studentsReportCardViewModel.LastFamilyName = studentData.LastFamilyName;
                            studentsReportCardViewModel.Gender = studentData.Gender;
                            studentsReportCardViewModel.GradeTitle = GradeLevelTitle;
                            studentsReportCardViewModel.Section = studentData.Sections != null ? studentData.Sections.Name : null;
                            studentsReportCardViewModel.HomeAddressLineOne = studentData.HomeAddressLineOne;
                            studentsReportCardViewModel.HomeAddressLineTwo = studentData.HomeAddressLineTwo;
                            studentsReportCardViewModel.HomeAddressCountry = studentData.HomeAddressCountry;
                            studentsReportCardViewModel.HomeAddressState = studentData.HomeAddressState;
                            studentsReportCardViewModel.HomeAddressCity = studentData.HomeAddressCity;
                            studentsReportCardViewModel.HomeAddressZip = studentData.HomeAddressZip;

                            if (schoolData.GradeScale.Count > 0)
                            {
                                schoolData.GradeScale.FirstOrDefault()!.Grade.ToList().ForEach(x => x.GradeScale = new());
                                studentsReportCardViewModel.gradeList = schoolData.GradeScale.FirstOrDefault()!.Grade.ToList();
                            }


                            var markingPeriodsData = reportCardViewModel.MarkingPeriods!.Split(",");
                            DateTime? startDate = null;
                            DateTime? endDate = null;
                            string? MarkingPeriodTitle = null;

                            foreach (var markingPeriod in markingPeriodsData)
                            {
                                MarkingPeriodDetailsForOtherTemplate markingPeriodDetailsForOtherTemplate = new MarkingPeriodDetailsForOtherTemplate();
                                int? QtrMarkingPeriodId = null;
                                int? SmstrMarkingPeriodId = null;
                                int? YrMarkingPeriodId = null;
                                int? PrgrsprdMarkingPeriodId = null;
                                bool? Exam = null;

                                if (markingPeriod != null)
                                {
                                    var markingPeriodid = markingPeriod.Split("_", StringSplitOptions.RemoveEmptyEntries);

                                    if (markingPeriodid.First() == "3")
                                    {
                                        PrgrsprdMarkingPeriodId = Int32.Parse(markingPeriodid.ElementAt(1));

                                        var ppData = this.context?.ProgressPeriods.FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.MarkingPeriodId == PrgrsprdMarkingPeriodId && x.AcademicYear == reportCardViewModel.AcademicYear);

                                        MarkingPeriodTitle = ppData!.ShortName;
                                        startDate = ppData.StartDate;
                                        endDate = ppData.EndDate;

                                        if (markingPeriodid.Last() == "E")
                                        {
                                            Exam = true;
                                            MarkingPeriodTitle = ppData!.ShortName + " Exam";
                                        }
                                    }

                                    if (markingPeriodid.First() == "2")
                                    {
                                        QtrMarkingPeriodId = Int32.Parse(markingPeriodid.ElementAt(1));

                                        var qtrData = this.context?.Quarters.FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.MarkingPeriodId == QtrMarkingPeriodId && x.AcademicYear == reportCardViewModel.AcademicYear);

                                        MarkingPeriodTitle = qtrData!.ShortName;
                                        startDate = qtrData.StartDate;
                                        endDate = qtrData.EndDate;

                                        if (markingPeriodid.Last() == "E")
                                        {
                                            Exam = true;
                                            MarkingPeriodTitle = qtrData!.ShortName + " Exam";
                                        }
                                    }

                                    if (markingPeriodid.First() == "1")
                                    {
                                        SmstrMarkingPeriodId = Int32.Parse(markingPeriodid.ElementAt(1));

                                        var smstrData = this.context?.Semesters.FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.MarkingPeriodId == SmstrMarkingPeriodId && x.AcademicYear == reportCardViewModel.AcademicYear);

                                        MarkingPeriodTitle = smstrData!.ShortName;
                                        startDate = smstrData.StartDate;
                                        endDate = smstrData.EndDate;

                                        if (markingPeriodid.Last() == "E")
                                        {
                                            Exam = true;
                                            MarkingPeriodTitle = smstrData!.ShortName + " Exam";
                                        }
                                    }

                                    if (markingPeriodid.First() == "0")
                                    {
                                        YrMarkingPeriodId = Int32.Parse(markingPeriodid.ElementAt(1));

                                        var yrData = this.context?.SchoolYears.FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.MarkingPeriodId == YrMarkingPeriodId && x.AcademicYear == reportCardViewModel.AcademicYear);

                                        MarkingPeriodTitle = yrData!.ShortName;
                                        startDate = yrData.StartDate;
                                        endDate = yrData.EndDate;

                                        if (markingPeriodid.Last() == "E")
                                        {
                                            Exam = true;
                                            MarkingPeriodTitle = yrData!.ShortName + " Exam";
                                        }
                                    }

                                    markingPeriodDetailsForOtherTemplate.MarkingPeriodShortName = MarkingPeriodTitle;
                                    var reportCardData = new List<StudentFinalGrade>();
                                    if (Exam == true)
                                    {
                                        reportCardData = this.context?.StudentFinalGrade.Include(x => x.StudentFinalGradeComments).Include(x => x.StudentFinalGradeStandard).Where(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.StudentId == student.StudentId && x.AcademicYear == reportCardViewModel.AcademicYear && ((x.YrMarkingPeriodId != null && x.YrMarkingPeriodId == YrMarkingPeriodId) || (x.SmstrMarkingPeriodId != null && x.SmstrMarkingPeriodId == SmstrMarkingPeriodId) || (x.QtrMarkingPeriodId != null && x.QtrMarkingPeriodId == QtrMarkingPeriodId) || (x.PrgrsprdMarkingPeriodId != null && x.PrgrsprdMarkingPeriodId == PrgrsprdMarkingPeriodId)) && x.IsExamGrade == true).ToList();
                                    }
                                    else
                                    {
                                        reportCardData = this.context?.StudentFinalGrade.Include(x => x.StudentFinalGradeComments).Include(x => x.StudentFinalGradeStandard).Where(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.StudentId == student.StudentId && x.AcademicYear == reportCardViewModel.AcademicYear && ((x.YrMarkingPeriodId != null && x.YrMarkingPeriodId == YrMarkingPeriodId) || (x.SmstrMarkingPeriodId != null && x.SmstrMarkingPeriodId == SmstrMarkingPeriodId) || (x.QtrMarkingPeriodId != null && x.QtrMarkingPeriodId == QtrMarkingPeriodId) || (x.PrgrsprdMarkingPeriodId != null && x.PrgrsprdMarkingPeriodId == PrgrsprdMarkingPeriodId)) && x.IsExamGrade != true).ToList();
                                    }

                                    decimal? SumofGPaValue = 0.0m;
                                    decimal? CreditEarned = 0.0m;
                                    decimal? CreditHours = 0.0m;
                                    int CourseCount = 0;
                                    if (reportCardData?.Any() == true)
                                    {
                                        foreach (var reportCard in reportCardData)
                                        {
                                            decimal? gPaValue = 0.0m;
                                            CourseCount++;
                                            CourseSectionGradeDetailsForOtherTemplate courseSectionGradeDetailsForOtherTemplate = new CourseSectionGradeDetailsForOtherTemplate();

                                            var CourseSectionData = this.context?.CourseSection.Include(s => s.GradeScale).FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.CourseSectionId == reportCard.CourseSectionId && x.CourseId == reportCard.CourseId);

                                            var gradeData = CourseSectionData?.GradeScale?.Grade.AsEnumerable().Where(x => x.TenantId == reportCard.TenantId && x.SchoolId == reportCard.SchoolId && String.Compare(x.Title, reportCard.GradeObtained, true) == 0 && x.GradeScaleId == reportCard.GradeScaleId).FirstOrDefault();

                                            if (gradeData != null)
                                            {
                                                CreditHours = CourseSectionData?.CreditHours;
                                                CreditEarned = reportCard.CreditEarned != null ? reportCard.CreditEarned : CourseSectionData?.CreditHours;
                                                gPaValue = CourseSectionData?.IsWeightedCourse != true ? gradeData.UnweightedGpValue * (CreditHours / CreditEarned) : gradeData.WeightedGpValue * (CreditHours / CreditEarned);
                                                SumofGPaValue = SumofGPaValue + gPaValue;

                                            }

                                            courseSectionGradeDetailsForOtherTemplate.MarkingPeriodShortName = MarkingPeriodTitle;
                                            courseSectionGradeDetailsForOtherTemplate.CourseSectionName = CourseSectionData?.CourseSectionName;
                                            courseSectionGradeDetailsForOtherTemplate.Grade = reportCard.GradeObtained;
                                            courseSectionGradeDetailsForOtherTemplate.Percentage = reportCard.PercentMarks.ToString();
                                            markingPeriodDetailsForOtherTemplate.courseSectionGradeDetailsForOtherTemplates.Add(courseSectionGradeDetailsForOtherTemplate);
                                        }

                                        if (SumofGPaValue > 0 && CourseCount > 0)
                                        {
                                            var Gpavalue = Math.Round((decimal)(SumofGPaValue / CourseCount), 2);
                                            markingPeriodDetailsForOtherTemplate.GPA = Gpavalue.ToString();
                                        }
                                    }

                                    var attendanceData = this.context?.AttendanceCodeCategories.Include(x => x.AttendanceCode).FirstOrDefault(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.AcademicYear == reportCardViewModel.AcademicYear);

                                    if (attendanceData != null)
                                    {
                                        foreach (var Attendance in attendanceData.AttendanceCode.ToList())
                                        {
                                            AttendanceDetailsForOtherTemplate attendanceDetailsForOtherTemplate = new AttendanceDetailsForOtherTemplate();

                                            var studentDailyAttendanceCount = this.context?.StudentDailyAttendance.Where(x => x.TenantId == reportCardViewModel.TenantId && x.SchoolId == reportCardViewModel.SchoolId && x.StudentId == student.StudentId && x.AttendanceCode == Attendance.Title && x.AttendanceDate >= startDate && x.AttendanceDate <= endDate).ToList().Count;
                                            attendanceDetailsForOtherTemplate.AttendanceTitle = Attendance.Title;
                                            attendanceDetailsForOtherTemplate.AttendanceCount = studentDailyAttendanceCount;
                                            attendanceDetailsForOtherTemplate.MarkingPeriodShortName = MarkingPeriodTitle;
                                            markingPeriodDetailsForOtherTemplate.attendanceDetailsForOtherTemplates.Add(attendanceDetailsForOtherTemplate);
                                        }
                                        studentsReportCardViewModel.markingPeriodDetailsForOtherTemplates.Add(markingPeriodDetailsForOtherTemplate);
                                    }
                                }
                            }
                            reportCardView.studentsReportCardViewModelList.Add(studentsReportCardViewModel);
                        }
                    }
                }
                else
                {
                    reportCardView._failure = true;
                    reportCardView._message = "Select Student Please";
                }
            }
            catch (Exception es)
            {
                reportCardView._failure = true;
                reportCardView._message = es.Message;
            }
            return reportCardView;
        }

    }
}
