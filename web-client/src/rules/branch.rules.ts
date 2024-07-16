import * as z from 'zod'

export const CreateBranchSchema = z.object({
  email: z.string().email('Email không đúng định dạng').min(1, 'Email là bắt buộc'),
  address: z.string().min(1, 'Địa chỉ là bắt buộc'),
  isMainBranch: z.boolean(),
  phone: z.string().regex(/^\d{10}$/, 'Số điện thoại phải có 10 chữ số')
})

export type CreateBranchFormType = z.infer<typeof CreateBranchSchema>
