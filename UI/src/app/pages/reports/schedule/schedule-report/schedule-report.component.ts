import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import icMenuBook from '@iconify/icons-ic/twotone-menu-book';
import { Router } from '@angular/router';

@Component({
  selector: 'vex-schedule-report',
  templateUrl: './schedule-report.component.html',
  styleUrls: ['./schedule-report.component.scss']
})
export class ScheduleReportComponent implements OnInit {

  icMenuBook = icMenuBook;

  constructor(public translateService: TranslateService, private router: Router) { }

  ngOnInit(): void {
  }

  viewDetails() {
    this.router.navigate(['/school', 'reports', 'schedule', 'schedule-report', 'schedule-report-details']);
  }

}
