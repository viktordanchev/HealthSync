import * as Yup from 'yup';
import { authErrors } from "../constants/errors";

export const validateFirstName = Yup.string()
    .required('First name' + authErrors.RequiredField);

export const validateLastName = Yup.string()
    .required('Last name' + authErrors.RequiredField);

export const validateEmail = Yup.string()
    .email(authErrors.InvalidEmail)
    .required('Email' + authErrors.RequiredField);

export const validatePassword = Yup.string()
    .min(6, 'Password must be at least 6 characters')
    .required('Password' + authErrors.RequiredField);

export const validateConfirmPassword = Yup.string()
    .required('Confirm password' + authErrors.RequiredField)
    .oneOf([Yup.ref('password'), null], 'Passwords must match');

export const validateVrfCode = Yup.string()
    .required('Code is required');