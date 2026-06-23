import { useState } from 'react';
import { Block } from '../../common-elements/Block/Block';
import styles from './FAQBlock.module.css'
import TypewriterText from '../../common-elements/TypewriterText/TypewriterText';
import questions from './faqData.json';

interface Question {
    question: string;
    answer: string;
}

export const FAQBlock = () => {
    const [selectedQuestion, setSelectedQuestion] = useState<Question | null>(null);

  return (
    <Block title='Остались вопросы?'>
        <div className={styles.content}>
            <div className={styles.questions}>
                {questions.map((question) => (
                    <div 
                        key={question.question}
                        className={`${styles.question} ${selectedQuestion === question ? styles.selected : ''}`} 
                        onClick={() => {setSelectedQuestion(question)}}
                    >
                        <span className={styles.questionText}>{question.question}</span>
                    </div>
                ))}
            </div>
            <div className={styles.answer}>
                {selectedQuestion && (
                    <TypewriterText 
                        className={styles.answerText} 
                        text={selectedQuestion?.answer} 
                        speed={30}
                    />
                )}
            </div>
        </div>
    </Block>
  );
}