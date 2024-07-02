import * as z from 'zod'

export const CreateEventSchema = z
  .object({
    name: z.string().min(1, { message: 'Tên sự kiện không được để trống.' }),
    startDate: z.string().refine((data) => new Date(data) > new Date(), { message: 'Ngày bắt đầu phải ở tương lai.' }),
    endDate: z.string(),
    location: z.string().min(1, 'Địa điểm không được để trống.'),
    description: z.string(),
    image: z.string()
  })
  .refine((data) => new Date(data.endDate) > new Date(data.startDate), {
    message: 'Ngày kết thúc không được sớm hơn ngày bắt đầu.',
    path: ['endDate']
  })

export type CreateEventFormType = z.infer<typeof CreateEventSchema>
