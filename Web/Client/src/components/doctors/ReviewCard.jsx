import React from 'react';

function ReviewCard({ data }) {
    return (
        <article className="w-full h-96 rounded-xl border border-solid border-white">
            <div className="flex flex-col justify-between">
                <p>{data.reviewer}</p>
                <p>{data.rating}</p>
            </div>
        </article>
    );
}

export default ReviewCard;