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
                    THANKS for selecting a loan
                </h1>

                <p className={styles.description}>
                    An Email has been sent to you from the bank
                </p>
            </main>
        </div>
    )
}

export default Home
