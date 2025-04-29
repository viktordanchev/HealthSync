<img src="https://capsule-render.vercel.app/api?type=waving&height=300&color=01bfa5&text=HealthSync&section=header&reversal=false&textBg=false&fontAlignY=35&fontColor=ffffff"/>

## Live Demo
- Deploy
  - The whole project is deployed in Railway.
    - Access:
       - Client - https://healthsync-client.up.railway.app
       - API (Swagger) - https://healthsync-restapi.up.railway.app
  - All images are stored in Azure Blob storage.

## Accounts
| Email | Password |
|-------|----------|
| i.ivanov@mail.com | 123456 |
| m.marinova@mail.com | 123456 |
| a.kirilov@mail.com | 123456 |
| k.conev@mail.com | 123456 |
| i.ivanova@mail.com | 123456 |
| m.kirilova@mail.com | 123456 |
| v.yankova@mail.com | 123456 |

## Functionality
- Chat
  - We have integrated chat communication with SignalR. You can send not only messages, but photos too.
   <div>
    <img src="https://github.com/user-attachments/assets/a2f723b7-df45-4297-bcbe-88ae32388840" width="250" height="300"><br>
    <p>Example of chat message with photo</p>
   </div>
- Emails Sender - You will receive emails from: viktordanchev03@gmail.com - my personal email. I am using it, because i have to pay for domain if i want to use another.
  - When you try to make registration you will receive email for verification code.
  - For recover password you will receive reset link.

## Tech Stack
- Back-End
  - .Net 8
  - ASP.NET Core
  - Entity Framework Core
  - SignalR
- Front-End
   - React
   - TailwindCSS
- Database
   - PostgreSQL
