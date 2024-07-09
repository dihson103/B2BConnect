import { ApiSuccessResponse } from "./util.types"

export type LoginType = {
    email: string
    password: string
}

export type LoginResponse = {
    accessToken: string,
    refreshToken: string,
    account: AccountResponse
}

export type AccountResponse = {
    email: string, 
    id: string,
    isAdmin: boolean
}
export type LoginSuccessResponse = ApiSuccessResponse<LoginResponse>

export type RegisterType = {
    email: string,
    password: string, 
    retypedPassword: string
}

export type RegisterSuccessResponse = ApiSuccessResponse<null>

export type LogoutSuccessResponse = ApiSuccessResponse<null>