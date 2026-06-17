import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface LoanCalculationRequest {
  amount: number;
  termInMonths: number;
  monthlyInterestRate: number;
  bsmvRate?: number;
  kkdfRate?: number;
}

export interface InstallmentDetail {
  installmentNumber: number;
  paymentAmount: number;
  principal: number;
  interest: number;
  bsmv: number;
  kkdf: number;
  remainingPrincipal: number;
}

export interface LoanCalculationResponse {
  monthlyInstallment: number;
  totalPayment: number;
  totalInterest: number;
  annualEffectiveRate: number;
  schedule: InstallmentDetail[];
}

@Injectable({
  providedIn: 'root'
})
export class LoanService {
  // Canlı ortam Render API URL'i
  private apiUrl = 'https://kredi-hesaplama-api.onrender.com/api/loan/calculate'; 

  constructor(private http: HttpClient) { }

  calculate(request: LoanCalculationRequest): Observable<LoanCalculationResponse> {
    return this.http.post<LoanCalculationResponse>(this.apiUrl, request);
  }
}
