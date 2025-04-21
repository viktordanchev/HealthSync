import React, { useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCaretUp, faCaretDown } from "@fortawesome/free-solid-svg-icons";

function FAQsSection() {
    const [activeIndex, setActiveIndex] = useState(null);
    const faqs = [
        { question: 'What is a HealthSync?', answer: 'HealthSync is a platform designed to simplify the connection between patients and doctors. It makes scheduling appointments easier for doctors and helps patients quickly find the right medical professional. The platform also offers a built-in chat feature, allowing patients to communicate directly with their doctors, including the ability to send images for better clarity and diagnosis.' },
        { question: 'Is there a mobile app?', answer: 'No, there is not a mobile app at the moment. We are currently focused on the web platform, as we believe the future lies in powerful, accessible, and responsive web solutions that work seamlessly across all devices.' },
        { question: 'Can anyone become a doctor on the platform?', answer: 'Yes, it is free and easy to get started as a doctor on HealthSync. We have made the process simple and accessible so that medical professionals can quickly join and start helping patients.' },
    ];

    const toggleFAQ = (index) => {
        setActiveIndex(activeIndex === index ? null : index);
    };

    return (
        <section className="w-full h-full flex space-x-6 text-gray-700 md:flex-col md:space-x-0 md:space-y-6 sm:flex-col sm:space-x-0 sm:space-y-6">
            <article className="w-1/3 p-6 bg-maincolor rounded-e-xl flex flex-col justify-center text-center space-y-12 md:space-y-6 md:w-full md:rounded-xl sm:space-y-6 sm:w-full sm:rounded-xl">
                <h2 className="text-5xl font-bold lg:text-4xl md:text-3xl sm:text-2xl">
                    Need Help?
                </h2>
                <p className="text-lg lg:text-base md:text-base sm:text-base">
                    If you have questions about how the website works or need more information, feel free to reach out. We're always happy to assist you!
                </p>
            </article>
            <article className="w-2/3 p-6 bg-maincolor rounded-s-xl md:w-full md:rounded-xl sm:w-full sm:rounded-xl">
                <div className="space-y-6 p-6 bg-white bg-opacity-75 border border-zinc-500 rounded-xl">
                    <div className="text-center space-y-2">
                        <h1 className="text-3xl lg:text-2xl md:text-xl sm:text-xl">Frequently Asked Questions</h1>
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
                                    <p className="mt-2 text-gray-900">- {faq.answer}</p>
                                )}
                            </li>
                        ))}
                    </ul>
                </div>
            </article>
        </section>
    );
}

export default FAQsSection;