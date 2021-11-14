import Head from 'next/head'
import styles from '../styles/Home.module.css'
import { useState } from 'react'
import { v4 as uuidv4 } from 'uuid';

function Home() {
  return (
    <div className={styles.container}>
      <Head>
        <title>Bank Loans</title>
        <meta name="description" content="Request Bank Loans From Multiple Banks" />
        <link rel="icon" href="/favicon.ico" />
      </Head>

      <main className={styles.main}>
        <h1 className={styles.title}>
          Welcome to <a href="localhost:3000">Bank Loans</a>
        </h1>

        <p className={styles.description}>
          Get started by filling out the boxes below
        </p>

        <div>
          <Form />
        </div>
      </main>
    </div>
  )
}

export default Home

function Form() {
  const requestLoan = async event => {
    event.preventDefault()

    const uuid = uuidv4();
    localStorage.setItem("user", uuid)

    const res = await fetch('http://localhost:90/loan/request/' + uuid, {
      body: JSON.stringify({
        amount: parseFloat(event.target.amount.value),
        startMonth: Number(event.target.startMonth.value),
        endMonth: Number(event.target.endMonth.value)
      }),
      headers: {
        'Content-Type': 'application/json'
      },
      method: 'POST',
      mode: "no-cors"
    })

    const result = await res
    console.log(result)
  }

  return (
    <form onSubmit={requestLoan}>
      <input id="amount" placeholder="Loan Amount" className="form-control" type="number" autoComplete="amount" required />
      <input id="startMonth" placeholder="Least Month to Pay" className="form-control" type="number" autoComplete="startMonth" required />
      <input id="endMonth" placeholder="Max Month to Pay" className="form-control" type="number" autoComplete="endMonth" required />
      <button type="submit" className="btn btn-primary"> Get Loans</button>
    </form>
  )
}