import React, { useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCaretUp, faCaretDown } from "@fortawesome/free-solid-svg-icons";

function FAQsSection() {
    const [activeIndex, setActiveIndex] = useState(null);
    const faqs = [
        { question: 'What is a HealthSync?', answer: 'This is 1.' },
        { question: 'How can I request a new browser?', answer: 'This is 2.' },
        { question: 'Is there a mobile app?', answer: 'This is 3.' },
        { question: 'What about other Chromium browsers?', answer: 'This is 4.' },
    ];

    const toggleFAQ = (index) => {
        setActiveIndex(activeIndex === index ? null : index);
    };

    return (
        <section className="space-y-6 p-6 bg-zinc-400 bg-opacity-25 border border-zinc-500 rounded-xl shadow-xl shadow-gray-300">
            <div className="text-center space-y-2">
                <h1 className="text-3xl">Frequently Asked Questions</h1>
                <p className="font-light">
                    Here are some of our FAQs. If you have any other questions you
                    would like answered please feel free to contact us.
                </p>
            </div>
            <ul className="flex flex-col">
                {faqs.map((faq, index) => (
                    <li key={index}
                        className="cursor-pointer flex flex-col border-b border-zinc-500 py-3"
                        onClick={() => toggleFAQ(index)}>
                        <div className="flex justify-between items-center">
                            <p>{faq.question}</p>
                            <FontAwesomeIcon icon={activeIndex === index ? faCaretUp : faCaretDown} />
                        </div>
                        {activeIndex === index && (
                            <p className="mt-2 text-gray-800">- {faq.answer}</p>
                        )}
                    </li>
                ))}
            </ul>
        </section>
    );
}

export default FAQsSection;