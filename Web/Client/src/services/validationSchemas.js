﻿import * as Yup from 'yup';
import { authErrors } from "../constants/errors";

export const validateFirstName = Yup.string()
    .required('First name' + authErrors.RequiredField);

export const validateLastName = Yup.string()
    .required('Last name' + authErrors.RequiredField);

export const validateEmail = Yup.string()
    .email(authErrors.InvalidEmail)
    .required('Email' + authErrors.RequiredField);

export const validatePassword = Yup.string()
    .min(6, authErrors.InvalidPass)
    .required('Password' + authErrors.RequiredField);

export const validateLoginPassword = Yup.string()
    .required('Password' + authErrors.RequiredField);

export const validateConfirmPassword = Yup.string()
    .required('Confirm password' + authErrors.RequiredField)
    .oneOf([Yup.ref('password'), null], authErrors.PassMatch);

export const validateVrfCode = Yup.string()
    .required('Verification code' + authErrors.RequiredField);