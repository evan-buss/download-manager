import { Component, OnInit } from '@angular/core';
import { FormControl, Validators, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-dashboard-page',
  templateUrl: './dashboard-page.component.html',
  styles: [],
})
export class DashboardPageComponent implements OnInit {
  downloads: Download[] = [];
  invalidInput = false;

  submissionForm = this.fb.group({
    submission: ['', [Validators.required]]
  });

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void { }

  onSubmit(): void {
    console.log('submitted');
  }

  onBlur(): void { }
}

export interface Download {
  fileName: string;
  startDate: Date;
  type: string;
  progress: number;
}
