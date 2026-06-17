import { Component, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoanService, LoanCalculationResponse } from './loan.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  loanForm: FormGroup;
  result: LoanCalculationResponse | null = null;
  loading = false;
  error = '';

  constructor(
    private fb: FormBuilder, 
    private loanService: LoanService,
    private cdr: ChangeDetectorRef
  ) {
    this.loanForm = this.fb.group({
      amount: [100000, [Validators.required, Validators.min(1)]],
      termInMonths: [12, [Validators.required, Validators.min(1)]],
      monthlyInterestRate: [3.5, [Validators.required, Validators.min(0.01)]],
      bsmvRate: [0.15],
      kkdfRate: [0.15]
    });
  }

  onSubmit() {
    if (this.loanForm.invalid) return;

    this.loading = true;
    this.error = '';
    this.result = null;
    this.cdr.detectChanges();

    const rawValue = this.loanForm.value;
    const requestData = {
      ...rawValue,
      amount: parseFloat(rawValue.amount.toString().replace(',', '.')),
      termInMonths: parseInt(rawValue.termInMonths.toString()),
      monthlyInterestRate: parseFloat(rawValue.monthlyInterestRate.toString().replace(',', '.'))
    };

    this.loanService.calculate(requestData).subscribe({
      next: (res) => {
        this.result = res;
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Hata oluştu:', err);
        this.error = 'Hesaplama sırasında bir hata oluştu. Lütfen API\'nin çalıştığından emin olun.';
        this.loading = false;
        this.cdr.detectChanges();
      }
    });
  }

  formatCurrency(value: number): string {
    return new Intl.NumberFormat('tr-TR', { style: 'currency', currency: 'TRY' }).format(value);
  }
}
