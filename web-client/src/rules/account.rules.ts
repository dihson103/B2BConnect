import * as z from 'zod'

export const CreateAccountSchema = z
  .object({
    email: z.string().min(1, { message: 'Email không được để trống.' }),
    password: z.string().min(1, { message: 'Password không được để trống' }),
    retypedPassword: z.string().min(1, { message: 'Password nhập lại không được để trống' })
  })
  .refine((data) => data.password === data.retypedPassword, {
    message: 'Mật khẩu nhập lại không khớp.',
    path: ['retypedPassword']
  })

export type CreateAccountFormType = z.infer<typeof CreateAccountSchema>
