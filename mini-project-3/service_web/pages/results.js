import React, { useState, useEffect } from 'react'
import Head from 'next/head'
import Loans from '../components/loans'
import styles from '../styles/Home.module.css'
import debounce from 'lodash.debounce';

const Results = ({ data }) => {
    const [loansData, setLoansData] = useState([]);
    const [user, setUser] = useState({name: "", cpr: "", email: ""});

    useEffect(async () => {
        let { data } = await getLoans();
        setLoansData(data);
    }, []);

    const handleChange = debounce(async target => {
        setUser({ ...user, [target.id]: target.value });
    }, 1000);

    return (
        <div className={styles.container}>
            <Head>
                <title>Bank Loans</title>
                <meta name="description" content="Request Bank Loans From Multiple Banks" />
                <link rel="icon" href="/favicon.ico" />
            </Head>

            <main className={styles.main}>
                <h1 className={styles.title}>
                    Please select a Loan From Below
                </h1>

                <p className={styles.description}>
                    And Fill out these new boxes
                </p>

                <div className="container loans">
                    <div className="inputs">
                        <input type="text" className="form-control" placeholder="Name" id="name" onChange={e => handleChange(e.target)} />
                        <input type="number" className="form-control" placeholder="CPR" id="cpr" onChange={e => handleChange(e.target)} />
                        <input type="Email" className="form-control" placeholder="Email" id="email" onChange={e => handleChange(e.target)} />
                    </div>
                    <hr />
                    <Loans loans={loansData} user={user} />
                </div>
            </main>
        </div>
    )
}

export default Results

const getLoans = async () => {
    const uuid = localStorage.getItem("user")

    const res = await fetch('http://localhost:90/loan/get/' + uuid)
    const loans = await res.json()

    return { data: loans }
}