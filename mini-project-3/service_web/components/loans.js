import React, { useState, useEffect } from 'react'
import Router from 'next/router'

const Loans = ({ loans, user }) => {
    console.log(loans)
    return (
        <div className="container d-flex flex-wrap">
            {loans.map((loan) => (
                <form className="card" key={loan.loanId} onSubmit={selectLoan}>
                    <div className="card-body">
                        <h5 className="card-title">Amount: {loan.amount}</h5>
                        <ul className="card-text">
                            <li>Bank: {loan.bankId}</li>
                            <li>Month To Pay: {loan.monthToPay}</li>
                            <li>Interest: {loan.interest}</li>
                            <li>AOP: {loan.aop}</li>
                        </ul>
                        <button type="submit" className="btn btn-primary">Select</button>
                    </div>
                    <input type="hidden" id="amount" value={loan.amount} />
                    <input type="hidden" id="bankId" value={loan.bankId} />
                    <input type="hidden" id="loanId" value={loan.loanId} />
                    <input type="hidden" id="userId" value={loan.userId} />
                    <input type="hidden" id="interest" value={loan.interest} />
                    <input type="hidden" id="aop" value={loan.aop} />
                    <input type="hidden" id="monthToPay" value={loan.monthToPay} />
                    <input type="hidden" id="name" value={user.name} />
                    <input type="hidden" id="cpr" value={user.cpr} />
                    <input type="hidden" id="email" value={user.email} />
                </form>
            ))}
        </div>
    )
}

export default Loans

const selectLoan = async event => {
    event.preventDefault()

    const uuid = localStorage.getItem("user")

    const res = await fetch('http://localhost:90/loan/select/' + uuid, {
        body: JSON.stringify({
            amount: parseFloat(event.target.amount.value),
            bankId: event.target.bankId.value,
            loanId: event.target.loanId.value,
            userId: event.target.userId.value,
            interest: parseFloat(event.target.interest.value),
            AOP: parseFloat(event.target.aop.value),
            monthToPay: Number(event.target.monthToPay.value),
            name: event.target.name.value,
            cpr: event.target.cpr.value,
            email: event.target.email.value
        }),
        headers: {
            'Content-Type': 'application/json'
        },
        method: 'POST',
        mode: "no-cors"
    })
    Router.push('/thanks')
}