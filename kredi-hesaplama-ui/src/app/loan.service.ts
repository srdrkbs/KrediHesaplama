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
  // Mobil cihazda/emülatörde test ederken localhost yerine bilgisayarınızın IP adresini veya emülatör için 10.0.2.2 kullanmalısınız.
  private apiUrl = 'http://localhost:5197/api/loan/calculate'; 

  constructor(private http: HttpClient) { }

  calculate(request: LoanCalculationRequest): Observable<LoanCalculationResponse> {
    return this.http.post<LoanCalculationResponse>(this.apiUrl, request);
  }
}
