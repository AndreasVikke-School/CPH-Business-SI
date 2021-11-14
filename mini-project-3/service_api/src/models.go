package main

type Loan struct {
	UserID     string  `json:"userId"`
	BankID     string  `json:"bankId"`
	LoanID     string  `json:"loanId"`
	Amount     float64 `json:"amount"`
	MonthToPay int     `json:"monthToPay"`
	Interest   float64 `json:"interest"`
	Aop        float64 `json:"aop"`
}
